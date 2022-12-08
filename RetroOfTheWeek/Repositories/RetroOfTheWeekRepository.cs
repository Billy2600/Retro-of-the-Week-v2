using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RetroOfTheWeek.DTOs;
using RetroOfTheWeek.Contexts;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.ObjectPool;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace RetroOfTheWeek.Repositories
{
    public class RetroOfTheWeekRepository : IRetroOfTheWeekRepository
    {
        private readonly RetroOfTheWeekContext _context;
        private readonly IConfiguration _config;

        private const string pagebreakMarker = "<!-- pagebreak -->";

        #region Public methods
        public RetroOfTheWeekRepository(RetroOfTheWeekContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<PostDto> GetPost(int id)
        {
            var post = await _context.Posts
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return post;
        }

        public async Task<List<PostDto>> GetLatestPosts(int count, bool pagebreak)
        {
            var posts = await _context.Posts
                .OrderByDescending(p => p.Date)
                .ToListAsync();

            foreach(var post in posts)
            {
                if (pagebreak)
                {
                    post.Text = Regex.Unescape(post.Text.Split(pagebreakMarker).First());
                }
                else
                {
                    post.Text = post.Text.Replace(pagebreakMarker, string.Empty);
                }
            }

            return posts;
        }

        public async Task<PostDto> AddPost(PostDto post)
        {
            post.Date = DateTime.Now;
            post.Rating = 0;
            post.Views = 0;

            await _context.Posts.AddAsync(post);
            // Don't attempt to add/update poster
            _context.Entry(post.Poster).State = EntityState.Detached;
            // Need to add this to add post, call above marks whole post detached
            _context.Entry(post).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return await _context.Posts.OrderByDescending(p => p.Date).FirstOrDefaultAsync();
        }

        public async Task DeletePost(int id)
        {
            var post = _context.Posts.First(p => p.Id == id);
            // Don't attempt to remove poster
            _context.Entry(post.Poster).State = EntityState.Detached;
            // Need to add this to add post, call above marks whole post detached
            _context.Entry(post).State = EntityState.Modified;

            _context.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> LoginUser(string username, string password)
        {
            var user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null)
                return false;

            if(user.Password == EncryptPassword(user.Id, user.Username, password))
                return true;

            return false;
        }

        #endregion

        #region Private methods
        private string EncryptPassword(int userId, string username, string password)
        {
            var encryptedPassowrd = string.Empty;

            using (SHA1 sha1Hash = SHA1.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(userId.ToString("X") + username + password + _config["SecurityKey"]);
                var hashBytes = sha1Hash.ComputeHash(sourceBytes);
                encryptedPassowrd = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }

            return encryptedPassowrd.ToLower();
        }

        #endregion
    }
}
