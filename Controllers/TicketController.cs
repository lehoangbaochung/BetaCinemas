using BetaCinemas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BetaCinemas.Controllers
{
    public class TicketController : Controller
    {
        private readonly CinemaContext _context;

        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.ToListAsync());
        }
    }
}
