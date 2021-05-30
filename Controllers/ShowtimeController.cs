using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BetaCinemas.Models;

namespace BetaCinemas.Controllers
{
    public class ShowtimeController : Controller
    {
        private readonly CinemaContext context;

        public ShowtimeController(CinemaContext context)
        {
            this.context = context;
        }

        // GET: Showtime
        public async Task<IActionResult> Index()
        {
            var cinemaContext = context.Showtimes.Include(s => s.Movie).Include(s => s.Room);

            return View(await cinemaContext.ToListAsync());
        }

        // GET: Showtime/Select/5
        public async Task<IActionResult> Select(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaContext = context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Room)
                .Where(s => s.MovieId == id);

            return View(await cinemaContext.ToListAsync());
        }

        // GET: Showtime/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtime = await context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (showtime == null)
            {
                return NotFound();
            }

            return View(showtime);
        }

        // GET: Showtime/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(context.Movies, "Id", "Title");
            ViewData["RoomId"] = new SelectList(context.Rooms, "Id", "Id");
            return View();
        }

        // POST: Showtime/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,MovieId,ShowTime1,Is2D,IsSpecial")] Showtime showTime)
        {
            if (ModelState.IsValid)
            {
                context.Add(showTime);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(context.Movies, "Id", "Title", showTime.MovieId);
            ViewData["RoomId"] = new SelectList(context.Rooms, "Id", "Id", showTime.RoomId);
            return View(showTime);
        }

        // GET: Showtime/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtime = await context.Showtimes.FindAsync(id);
            if (showtime == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(context.Movies, "Id", "Title", showtime.MovieId);
            ViewData["RoomId"] = new SelectList(context.Rooms, "Id", "Id", showtime.RoomId);
            return View(showtime);
        }

        // POST: Showtime/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,MovieId,ShowTime1,Is2D,IsSpecial")] Showtime showtime)
        {
            if (id != showtime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(showtime);
                    await context.SaveChangesAsync();
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
            ViewData["MovieId"] = new SelectList(context.Movies, "Id", "Title", showtime.MovieId);
            ViewData["RoomId"] = new SelectList(context.Rooms, "Id", "Id", showtime.RoomId);
            return View(showtime);
        }

        // GET: Showtime/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtime = await context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (showtime == null)
            {
                return NotFound();
            }

            return View(showtime);
        }

        // POST: Showtime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showtime = await context.Showtimes.FindAsync(id);
            context.Showtimes.Remove(showtime);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowtimeExists(int id)
        {
            return context.Showtimes.Any(e => e.Id == id);
        }
    }
}
