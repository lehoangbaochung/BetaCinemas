using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BetaCinemas.Models;
using System;
using System.Security.Claims;

namespace BetaCinemas.Controllers
{
    public class ContactController : Controller
    {
        private readonly CinemaContext context;

        public ContactController(CinemaContext context)
        {
            this.context = context;
        }

        // GET: Contact
        public async Task<IActionResult> Index()
        {
            var cinemaContext = context.Contacts.Include(c => c.Member);
            return View(await cinemaContext.ToListAsync());
        }

        // GET: Contact/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await context.Contacts
                .Include(c => c.Member)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contact/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(context.Members, "Id", "Id");
            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,MemberId,SentTime,SentContent,ReplyTime,ReplyContent,IsReplied")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.MemberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                contact.SentTime = DateTime.Parse("2021/05/29 10:00:00");
                context.Database.ExecuteSqlRaw("");
                context.Add(contact);

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["MemberId"] = new SelectList(context.Members, "Id", "Id", contact.MemberId);

            return View(contact);
        }

        // GET: Contact/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(context.Members, "Id", "Id", contact.MemberId);
            return View(contact);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemberId,SentTime,SentContent,ReplyTime,ReplyContent,IsReplied")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(contact);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
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
            ViewData["MemberId"] = new SelectList(context.Members, "Id", "Id", contact.MemberId);
            return View(contact);
        }

        // GET: Contact/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await context.Contacts
                .Include(c => c.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await context.Contacts.FindAsync(id);
            context.Contacts.Remove(contact);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return context.Contacts.Any(e => e.Id == id);
        }
    }
}
