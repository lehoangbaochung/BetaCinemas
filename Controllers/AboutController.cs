using Microsoft.AspNetCore.Mvc;

namespace BetaCinemas.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Question()
        {
            return View();
        }

        public IActionResult Use()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
