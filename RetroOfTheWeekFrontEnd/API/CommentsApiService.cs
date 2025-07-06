using RetroOfTheWeekFrontEnd.API.Interfaces;
using RetroOfTheWeekShared.Models;

namespace RetroOfTheWeekFrontEnd.API
{
    public class CommentsApiService : RetroOfTheWeekApiService, ICommentsApiService
    {
        ILogger<CommentsApiService> _logger;

        public CommentsApiService(HttpClient httpClient, ILogger<CommentsApiService> logger, IConfiguration configuration) 
            : base(httpClient, logger, configuration)
        {
            _logger = logger;
        }

        public async Task<CommentModel[]?> GetCommentsForPost(int postId)
        {
            try
            {
                var endpoint = string.Format(EndpointPathConstants.Comments.GetCommentsForPost, postId);
                var comments = await GetAsync<CommentModel[]>(endpoint);
                return comments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception($"Error requesting comments for post with ID {postId}: {ex.Message}");
            }
        }
    }
}
