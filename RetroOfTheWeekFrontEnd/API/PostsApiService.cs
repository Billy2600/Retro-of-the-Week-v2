namespace RetroOfTheWeekFrontEnd.API
{
    public class PostsApiService : RetroOfTheWeekApiService
    {
        ILogger<PostsApiService> _logger;

        public PostsApiService(HttpClient httpClient, ILogger<PostsApiService> logger) : base(httpClient, logger)
        {
            _logger = logger;
        }
    }
}
