using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BetaCinemas.Models;

namespace BetaCinemas.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class TicketController : Controller
    {
        private readonly CinemaContext context;

        public TicketController(CinemaContext context)
        {
            this.context = context;
        }

        // GET: Administrator/Ticket
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public async Task<IActionResult> Index()
        {
            var cinemaContext = context.Tickets
                .Include(t => t.Showtime)
                .Include(t => t.TicketPrice);

            return View(await cinemaContext.ToListAsync());
        }

        // GET: Administrator/Ticket/Details/5
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await context.Tickets
                .Include(t => t.Showtime)
                .Include(t => t.TicketPrice)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Administrator/Ticket/Create
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public IActionResult Create()
        {
            //ViewData["MemberId"] = new SelectList(context.Members, "Id", "Id");
            ViewData["ShowtimeId"] = new SelectList(context.Showtimes, "Id", "Id");
            ViewData["TicketPriceId"] = new SelectList(context.TicketPrices, "Id", "DateType");
            return View();
        }

        // POST: Administrator/Ticket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemberId,ShowtimeId,TicketPriceId,SoldTime")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                context.Add(ticket);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["MemberId"] = new SelectList(context.Members, "Id", "Id", ticket.MemberId);
            ViewData["ShowtimeId"] = new SelectList(context.Showtimes, "Id", "Id", ticket.ShowtimeId);
            ViewData["TicketPriceId"] = new SelectList(context.TicketPrices, "Id", "DateType", ticket.TicketPriceId);
            return View(ticket);
        }

        // GET: Administrator/Ticket/Edit/5
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            //ViewData["MemberId"] = new SelectList(context.Members, "Id", "Id", ticket.MemberId);
            ViewData["ShowtimeId"] = new SelectList(context.Showtimes, "Id", "Id", ticket.ShowtimeId);
            ViewData["TicketPriceId"] = new SelectList(context.TicketPrices, "Id", "DateType", ticket.TicketPriceId);
            return View(ticket);
        }

        // POST: Administrator/Ticket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemberId,ShowtimeId,TicketPriceId,SoldTime")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(ticket);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(context.Members, "Id", "Id", ticket.MemberId);
            ViewData["ShowtimeId"] = new SelectList(context.Showtimes, "Id", "Id", ticket.ShowtimeId);
            ViewData["TicketPriceId"] = new SelectList(context.TicketPrices, "Id", "DateType", ticket.TicketPriceId);
            return View(ticket);
        }

        // GET: Administrator/Ticket/Delete/5
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await context.Tickets
                .Include(t => t.Member)
                .Include(t => t.Showtime)
                .Include(t => t.TicketPrice)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Administrator/Ticket/Delete/5
        [HttpPost("{area:exists}/{controller=Home}/{action=Index}/{id?}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await context.Tickets.FindAsync(id);
            context.Tickets.Remove(ticket);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return context.Tickets.Any(e => e.Id == id);
        }
    }
}
