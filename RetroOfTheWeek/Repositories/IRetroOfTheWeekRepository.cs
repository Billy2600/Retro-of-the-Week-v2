using RetroOfTheWeek.DTOs;
using RetroOfTheWeekAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroOfTheWeek.Repositories
{
    public interface IRetroOfTheWeekRepository
    {
        public Task<PostDto> GetPost(int id);

        Task<List<PostDto>> GetLatestPosts(int count, bool pagebreak);

        Task<PostDto> AddPost(PostDto post);

        Task DeletePost(int id);

        Task<bool> LoginUser(string username, string password);

        Task<List<CommentDto>> GetPostComments(int postId);
    }
}
