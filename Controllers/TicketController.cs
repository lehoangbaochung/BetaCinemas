using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BetaCinemas.Models;

namespace BetaCinemas.Controllers
{
    public class TicketController : Controller
    {
        private readonly CinemaContext _context;

        public TicketController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Ticket
        public async Task<IActionResult> Index()
        {
            //var cinemaContext = _context.Tickets.Include(t => t.Member).Include(t => t.Showtime).Include(t => t.TicketPrice);
            return View(await _context.Tickets.ToListAsync());
        }

        // GET: Ticket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
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

        // GET: Ticket/Create
        public IActionResult Create()
        {
            //ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
            //ViewData["ShowtimeId"] = new SelectList(_context.Showtimes, "Id", "Id");
            //ViewData["TicketPriceId"] = new SelectList(_context.TicketPrices, "Id", "DateType");
            return View();
        }

        // POST: Ticket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemberId,ShowtimeId,TicketPriceId,SoldTime")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", ticket.MemberId);
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes, "Id", "Id", ticket.ShowtimeId);
            ViewData["TicketPriceId"] = new SelectList(_context.TicketPrices, "Id", "DateType", ticket.TicketPriceId);
            return View(ticket);
        }

        // GET: Ticket/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", ticket.MemberId);
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes, "Id", "Id", ticket.ShowtimeId);
            ViewData["TicketPriceId"] = new SelectList(_context.TicketPrices, "Id", "DateType", ticket.TicketPriceId);
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
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
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", ticket.MemberId);
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes, "Id", "Id", ticket.ShowtimeId);
            ViewData["TicketPriceId"] = new SelectList(_context.TicketPrices, "Id", "DateType", ticket.TicketPriceId);
            return View(ticket);
        }

        // GET: Ticket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
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

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
