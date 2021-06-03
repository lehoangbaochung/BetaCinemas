using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetaCinemas.Models;
using System;
using BetaCinemas.Data.Contexts;

namespace BetaCinemas.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class PostController : Controller
    {
        private readonly CinemaContext context;

        public PostController(CinemaContext context)
        {
            this.context = context;
        }

        // GET: Administrator/Post
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public async Task<IActionResult> Index()
        {
            return View(await context.Posts.ToListAsync());
        }

        // GET: Administrator/Post/Details/5
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Administrator/Post/Create
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Post/Create
        [HttpPost("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostTime,Title,Content,Genre,AttachedUrl,ImageUrl")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                context.Add(post);

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }

        // GET: Administrator/Post/Edit/5
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Administrator/Post/Edit/5
        [HttpPost("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostTime,Title,Content,Genre,AttachedUrl,ImageUrl")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(post);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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

            return View(post);
        }

        // GET: Administrator/Post/Delete/5
        [HttpGet("{area:exists}/{controller=Home}/{action=Index}/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Administrator/Post/Delete/5
        [HttpPost("{area:exists}/{controller=Home}/{action=Index}/{id?}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await context.Posts.FindAsync(id);
            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return context.Posts.Any(e => e.Id == id);
        }
    }
}
