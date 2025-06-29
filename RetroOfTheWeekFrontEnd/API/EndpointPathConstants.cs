namespace RetroOfTheWeekFrontEnd.API
{
    public class EndpointPathConstants
    {
        public class Posts
        {
            public const string GetPosts = "api/posts";
            public const string GetPostById = "api/posts/{id}";
            public const string CreatePost = "api/posts";
            public const string UpdatePost = "api/posts/{id}";
            public const string DeletePost = "api/posts/{id}";
        }

        public class Auth
        {
            public const string RequestToken = "/Auth";
        }
    }
}
