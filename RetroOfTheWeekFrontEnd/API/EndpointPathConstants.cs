namespace RetroOfTheWeekFrontEnd.API
{
    public class EndpointPathConstants
    {
        public class Posts
        {
            public const string GetLatestPosts = "/Posts/Latest/{0}/{1}";
            public const string GetPost = "/Posts/{0}";
        }

        public class Auth
        {
            public const string RequestToken = "/Auth";
        }

        public class Comments
        {
            public const string GetCommentsForPost = "/Comments/Post/{0}";
        }
    }
}
