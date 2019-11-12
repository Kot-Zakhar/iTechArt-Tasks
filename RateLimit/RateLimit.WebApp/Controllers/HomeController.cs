using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RateLimit.WebApp.Models;
using RateLimit.WebApp.Services;

namespace RateLimit.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProfileService _profileService;

        public HomeController(ILogger<HomeController> logger, ProfileService profileService)
        {
            _logger = logger;
            _profileService = profileService;
        }

        public IActionResult Profile(int page = 0, int pageSize = 10)
        {
            var url = "/home/profile";
            ViewData["page"] = page + 1;
            ViewData["pageSize"] = pageSize;
            ViewData["url"] = url;
            ViewData["previousPageUrl"] = url + "?page=" + (page - 1) + "&pageSize=" + pageSize;
            ViewData["nextPageUrl"] = url + "?page=" + (page + 1) + "&pageSize=" + pageSize;
            return View(_profileService.GetPage(page, pageSize));
        }

        public IActionResult Index()
        {
            return Redirect("/Home/Profile");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
