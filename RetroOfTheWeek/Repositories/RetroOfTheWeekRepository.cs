using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RetroOfTheWeek.DTOs;
using RetroOfTheWeek.Contexts;

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
    }
}
