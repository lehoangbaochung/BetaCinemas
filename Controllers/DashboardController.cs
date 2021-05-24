using Microsoft.AspNetCore.Mvc;

namespace BetaCinemas.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
