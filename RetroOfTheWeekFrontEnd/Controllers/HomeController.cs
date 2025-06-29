using Microsoft.AspNetCore.Mvc;
using RetroOfTheWeek.Models;
using RetroOfTheWeekFrontEnd.API;
using RetroOfTheWeekFrontEnd.Models;
using System.Diagnostics;

namespace RetroOfTheWeekFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RetroOfTheWeekApiService _apiService;

        public HomeController(ILogger<HomeController> logger, RetroOfTheWeekApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var tokenRequest = new TokenRequestModel
            {
                Username = "billy",
                Password = "password"
            };

            var securityToken = await _apiService.GetSecurityToken(tokenRequest);
            return View();
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
