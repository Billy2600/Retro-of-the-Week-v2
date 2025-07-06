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
using Microsoft.Extensions.Configuration;

namespace RetroOfTheWeekTests.Repositories
{
    [TestClass]
    public class RetroOfTheWeekRepositoryTests
    {
        private Fixture _fixture;
        private SqliteConnection _connection;
        private DbContextOptions<RetroOfTheWeekContext> _contextOptions;
        private Mock<IConfiguration> _mockConfig;

        private const string pagebreakMarker = "<!-- pagebreak -->";

        public RetroOfTheWeekRepositoryTests()
        {
            _fixture = new Fixture();
            _mockConfig = new Mock<IConfiguration>();
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

                var retroOfTheWekRepo = new RetroOfTheWeekRepository(context, _mockConfig.Object);

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
            var posts = _fixture.CreateMany<PostDto>(10).OrderByDescending(p => p.Date).ToList();

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

                var retroOfTheWekRepo = new RetroOfTheWeekRepository(context, _mockConfig.Object);

                // Act
                var results = await retroOfTheWekRepo.GetLatestPosts(5, false);

                Assert.AreEqual(5, results.Count);

                // Assert
                for (int i = 0; i < results.Count(); i++)
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

                var retroOfTheWekRepo = new RetroOfTheWeekRepository(context, _mockConfig.Object);

                // Act
                var result = await retroOfTheWekRepo.AddPost(post);

                // Assert
                Assert.AreEqual(result, post);
            }
        }

        [TestMethod]
        public async Task DeletePost_HappyPath()
        {
            // Arrange
            var post = _fixture.Create<PostDto>();

            using (var context = new RetroOfTheWeekContext(_contextOptions))
            {
                if (!context.Database.EnsureCreated())
                    Assert.Fail("Context not created");

                context.Posts.Add(post);
                context.SaveChanges();

                var retroOfTheWekRepo = new RetroOfTheWeekRepository(context, _mockConfig.Object);

                // Act
                await retroOfTheWekRepo.DeletePost(post.Id);

                // Assert
                Assert.AreEqual(0, context.Users.Where(p => p.Id == post.Id).Count());
            }
        }

        [TestMethod]
        public async Task LoginUser_HappyPath()
        {
            // Arrange
            var user = _fixture.Create<UserDto>();
            user.Id = 73;
            user.Username = "Usernameb7e836cf-9953-40ff-99a8-307a7ff698db";
            user.Password = "8a13d66ce2561b4c0a815cf4b63dfbfe6e64b263";

            _mockConfig.Setup(x => x["SecurityKey"]).Returns("e9488dd9-1c81-48ed-9069-1a2505ab9792");

            using (var context = new RetroOfTheWeekContext(_contextOptions))
            {
                if (!context.Database.EnsureCreated())
                    Assert.Fail("Context not created");

                context.Users.Add(user);
                context.SaveChanges();

                var retroOfTheWekRepo = new RetroOfTheWeekRepository(context, _mockConfig.Object);

                // Act
                var result = await retroOfTheWekRepo.LoginUser(user.Username, "Passwordac65547e-1a14-4b14-a42d-9c116f1e3cf");

                // Assert
                Assert.IsTrue(result);
            }
        }
    }
}
