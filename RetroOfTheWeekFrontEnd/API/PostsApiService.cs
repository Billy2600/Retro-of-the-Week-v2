using RetroOfTheWeek.Models;
using RetroOfTheWeekFrontEnd.API.Interfaces;
using RetroOfTheWeekShared.Models;

namespace RetroOfTheWeekFrontEnd.API
{
    public class PostsApiService : RetroOfTheWeekApiService, IPostsApiService
    {
        ILogger<PostsApiService> _logger;

        public PostsApiService(HttpClient httpClient, ILogger<PostsApiService> logger, IConfiguration configuration) : base(httpClient, logger, configuration)
        {
            _logger = logger;
        }

        public async Task<PostModel[]?> GetLatestPosts(int numberOfPosts, bool pagebreak)
        {
            try
            {
                var endpoint = string.Format(EndpointPathConstants.Posts.GetLatestPosts, numberOfPosts, pagebreak);
                var posts = await GetAsync<PostModel[]>(endpoint);
                return posts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception($"Error requesting LatestPosts: {ex.Message}");
            }
        }
    }
}
