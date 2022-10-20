using Microsoft.AspNetCore.Mvc;
using Quiz.Web.Models;
using System.Diagnostics;

namespace Quiz.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies.ContainsKey("EditCookie") || HttpContext.Request.Cookies.ContainsKey("ViewCookie") || HttpContext.Request.Cookies.ContainsKey("RestrictedCookie"))
            {
                return View();
            }
            return RedirectToAction("login", "login");
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