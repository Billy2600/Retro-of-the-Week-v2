using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RetroOfTheWeek.DTOs;
using RetroOfTheWeek.Contexts;
using System.Runtime.InteropServices;

namespace RetroOfTheWeek.Repositories
{
    public class RetroOfTheWeekRepository : IRetroOfTheWeekRepository
    {
        private readonly RetroOfTheWeekContext _context;

        private const string pagebreakMarker = "<!-- pagebreak -->";

        public RetroOfTheWeekRepository(RetroOfTheWeekContext context)
        {
            _context = context;
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
    }
}
