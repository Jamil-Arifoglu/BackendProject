
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Foxic.Controllers
{
    public class FoxicController : Controller
    {
        private readonly ILogger<FoxicController> _logger;

        public FoxicController(ILogger<FoxicController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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