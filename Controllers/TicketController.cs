using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BetaCinemas.Models;
using System.Security.Claims;
using BetaCinemas.Data.Contexts;

namespace BetaCinemas.Controllers
{
    public class TicketController : Controller
    {
        private readonly CinemaContext context;
        private static int showtimeId;

        public TicketController(CinemaContext context)
        {
            this.context = context;
        }

        // GET: Ticket
        public async Task<IActionResult> Index()
        {
            var tickets = context.Tickets
                .Include(t => t.Showtime)
                .Where(t => t.MemberId.Equals(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return View(await tickets.ToListAsync());
        }

        // GET: Ticket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await context.Tickets
                .Include(t => t.Showtime)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Ticket/Create
        public IActionResult Create(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            showtimeId = id;

            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,SoldTime")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.ShowtimeId = showtimeId;
                ticket.Showtime = await context.Showtimes.FindAsync(showtimeId);
                ticket.TicketPrice = GetPrice(ticket);
                ticket.MemberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ticket.SoldTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                context.Add(ticket);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ticket);
        }

        // GET: Ticket/Edit/5
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
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,ShowtimeId,TicketPriceId,SoldTime")] Ticket ticket)
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
            //ViewData["MemberId"] = new SelectList(context.Members, "Id", "Id", ticket.MemberId);
            ViewData["ShowtimeId"] = new SelectList(context.Showtimes, "Id", "Id", ticket.ShowtimeId);
            return View(ticket);
        }

        // GET: Ticket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await context.Tickets
                .Include(t => t.Showtime)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
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

        private static int GetPrice(Ticket ticket)
        {
            int price = 50000;

            if (ticket.Showtime.Is2D == false)
                price += 10000;

            if (ticket.Showtime.IsSpecial == true)
                price += 15000;

            if (ticket.Showtime.Times.DayOfWeek.ToString() == "6" || 
                ticket.Showtime.Times.DayOfWeek.ToString() == "0")
                price += 10000;

            return price;
        }
    }
}
