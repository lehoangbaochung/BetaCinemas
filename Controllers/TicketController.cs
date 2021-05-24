using Microsoft.AspNetCore.Mvc;

namespace BetaCinemas.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
