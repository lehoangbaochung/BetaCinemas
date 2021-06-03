using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BetaCinemas.Models;
using BetaCinemas.Data.Contexts;

namespace BetaCinemas.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ShowtimeController : Controller
    {
        private readonly CinemaContext _context;

        public ShowtimeController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Administrator/Showtime
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public async Task<IActionResult> Index()
        {
            var cinemaContext = _context.Showtimes.Include(s => s.Movie).Include(s => s.Room);
            return View(await cinemaContext.ToListAsync());
        }

        // GET: Administrator/Showtime/Details/5
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtime = await _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (showtime == null)
            {
                return NotFound();
            }

            return View(showtime);
        }

        // GET: Administrator/Showtime/Create
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id");
            return View();
        }

        // POST: Administrator/Showtime/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,MovieId,Times,Is2D,IsSpecial")] Showtime showtime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(showtime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", showtime.MovieId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", showtime.RoomId);
            return View(showtime);
        }

        // GET: Administrator/Showtime/Edit/5
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtime = await _context.Showtimes.FindAsync(id);
            if (showtime == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", showtime.MovieId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", showtime.RoomId);
            return View(showtime);
        }

        // POST: Administrator/Showtime/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,MovieId,Times,Is2D,IsSpecial")] Showtime showtime)
        {
            if (id != showtime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(showtime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowtimeExists(showtime.Id))
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
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", showtime.MovieId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", showtime.RoomId);
            return View(showtime);
        }

        // GET: Administrator/Showtime/Delete/5
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtime = await _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (showtime == null)
            {
                return NotFound();
            }

            return View(showtime);
        }

        // POST: Administrator/Showtime/Delete/5
        [HttpPost("{area:exists}/{controller=Home}/{action=Index}/{id?}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showtime = await _context.Showtimes.FindAsync(id);
            _context.Showtimes.Remove(showtime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowtimeExists(int id)
        {
            return _context.Showtimes.Any(e => e.Id == id);
        }
    }
}
