using Microsoft.AspNetCore.Mvc;
using RetroOfTheWeekShared.Models;
using RetroOfTheWeekFrontEnd.API.Interfaces;
using RetroOfTheWeekShared.Models;

namespace RetroOfTheWeekFrontEnd.Controllers
{
    [Route("p")] // For compatibility with the original website
    public class PostsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostsApiService _postsApiService;
        private readonly ICommentsApiService _commentsApiService;

        public PostsController(ILogger<HomeController> logger, IPostsApiService postsApiService, ICommentsApiService commentsApiService)
        {
            _logger = logger;
            _postsApiService = postsApiService;
            _commentsApiService = commentsApiService;
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            var post = await _postsApiService.GetPost(id);
            var comments = (await _commentsApiService.GetCommentsForPost(id))?.ToList();

            return View(new Tuple<PostModel, List<CommentModel>>(post, comments));
        }
    }
}
