using Microsoft.AspNetCore.Mvc;
using RetroOfTheWeek.Models;
using RetroOfTheWeekFrontEnd.API;
using RetroOfTheWeekFrontEnd.API.Interfaces;
using RetroOfTheWeekFrontEnd.Models;
using System.Diagnostics;

namespace RetroOfTheWeekFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostsApiService _postsApiService;

        public HomeController(ILogger<HomeController> logger, IPostsApiService postsApiService)
        {
            _logger = logger;
            _postsApiService = postsApiService;
        }

        public async Task<IActionResult> Index()
        {
            var latestPosts = await _postsApiService.GetLatestPosts(5, false);
            return View(latestPosts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
