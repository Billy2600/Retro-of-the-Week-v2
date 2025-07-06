using RetroOfTheWeek.Models;

namespace RetroOfTheWeekFrontEnd.API.Interfaces
{
    public interface IPostsApiService
    {
        Task<PostModel[]?> GetLatestPosts(int numberOfPosts, bool pagebreak);
        Task<PostModel?> GetPost(int id);
    }
}
