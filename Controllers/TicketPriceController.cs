using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetaCinemas.Models;

namespace BetaCinemas.Controllers
{
    public class TicketPriceController : Controller
    {
        private readonly CinemaContext _context;

        public TicketPriceController(CinemaContext context)
        {
            _context = context;
        }

        // GET: TicketPrice
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketPrices.ToListAsync());
        }

        // GET: TicketPrice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketPrice = await _context.TicketPrices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketPrice == null)
            {
                return NotFound();
            }

            return View(ticketPrice);
        }

        // GET: TicketPrice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketPrice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateType,StartTime,EndTime,Price,IsPriority,Is2D,IsSpecial")] TicketPrice ticketPrice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketPrice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketPrice);
        }

        // GET: TicketPrice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketPrice = await _context.TicketPrices.FindAsync(id);
            if (ticketPrice == null)
            {
                return NotFound();
            }
            return View(ticketPrice);
        }

        // POST: TicketPrice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateType,StartTime,EndTime,Price,IsPriority,Is2D,IsSpecial")] TicketPrice ticketPrice)
        {
            if (id != ticketPrice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketPrice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketPriceExists(ticketPrice.Id))
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
            return View(ticketPrice);
        }

        // GET: TicketPrice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketPrice = await _context.TicketPrices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketPrice == null)
            {
                return NotFound();
            }

            return View(ticketPrice);
        }

        // POST: TicketPrice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketPrice = await _context.TicketPrices.FindAsync(id);
            _context.TicketPrices.Remove(ticketPrice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketPriceExists(int id)
        {
            return _context.TicketPrices.Any(e => e.Id == id);
        }
    }
}
