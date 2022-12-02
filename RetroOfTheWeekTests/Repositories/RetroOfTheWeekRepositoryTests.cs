using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using RetroOfTheWeek.Repositories;
using RetroOfTheWeek.DTOs;
using RetroOfTheWeek.Models;
using AutoMapper;
using RetroOfTheWeek.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Infrastructure;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Caching.Memory;

namespace RetroOfTheWeekTests.Repositories
{
    [TestClass]
    public class RetroOfTheWeekRepositoryTests
    {
        private readonly Mapper _mapper;
        private Fixture _fixture;
        private SqliteConnection _connection;
        private DbContextOptions<RetroOfTheWeekContext> _contextOptions;

        private const string pagebreakMarker = "<!-- pagebreak -->";

        public RetroOfTheWeekRepositoryTests()
        {
            _fixture = new Fixture();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _connection.Dispose();
        }

        [TestInitialize]
        public void Setup()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            _contextOptions = new DbContextOptionsBuilder<RetroOfTheWeekContext>()
                .UseSqlite(_connection)
                .Options;
        }

        [TestMethod]
        public async Task GetPost_HappyPath()
        {
            // Arrange
            var post = _fixture.Create<PostDto>();
            post.Id = 1;

            using (var context = new RetroOfTheWeekContext(_contextOptions))
            {
                if (!context.Database.EnsureCreated())
                    Assert.Fail("Context not created");

                context.Posts.Add(post);
                context.SaveChanges();

                var retroOfTheWekRepo = new RetroOfTheWeekRepository(context);

                // Act
                var result = await retroOfTheWekRepo.GetPost(1);

                // Assert
                Assert.AreEqual(post, result);
            }
        }

        [TestMethod]
        public async Task GetLatestPosts_HappyPath()
        {
            // Arrange
            var posts = _fixture.CreateMany<PostDto>(5).OrderByDescending(p => p.Date).ToList();

            foreach (var post in posts)
            {
                post.Text += pagebreakMarker;
            }

            using (var context = new RetroOfTheWeekContext(_contextOptions))
            {
                if (!context.Database.EnsureCreated())
                    Assert.Fail("Context not created");

                context.Posts.AddRange(posts);
                context.SaveChanges();

                var retroOfTheWekRepo = new RetroOfTheWeekRepository(context);

                // Act
                var results = await retroOfTheWekRepo.GetLatestPosts(5, false);

                // Assert
                for(int i = 0; i < posts.Count(); i++)
                {
                    Assert.AreEqual(posts[i], results[i]);
                    Assert.AreEqual(-1, results[i].Text.IndexOf(pagebreakMarker));
                }
            }
        }

        [TestMethod]
        public async Task AddPost_HappyPath()
        {
            // Arrange
            var user = _fixture.Create<UserDto>(); // Need to already have a user to map to in the DB
            var post = _fixture.Create<PostDto>();
            post.PosterId = user.Id;
            post.Poster = user;

            using (var context = new RetroOfTheWeekContext(_contextOptions))
            {
                if (!context.Database.EnsureCreated())
                    Assert.Fail("Context not created");

                context.Users.Add(user);
                context.SaveChanges();

                var retroOfTheWekRepo = new RetroOfTheWeekRepository(context);

                // Act
                var result = await retroOfTheWekRepo.AddPost(post);

                // Assert
                Assert.AreEqual(result, post);
            }
        }
    }
}
