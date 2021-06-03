using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetaCinemas.Models;
using System;
using System.Security.Claims;
using BetaCinemas.Data.Contexts;

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
            var contacts = context.Contacts.Where(c => c.MemberId.Equals(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return View(await contacts.ToListAsync());
        }

        // GET: Contact/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await context.Contacts
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
                contact.IsReplied = false;
                contact.MemberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                contact.SentTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                context.Add(contact);

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

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
                contact.IsReplied = false;
                contact.MemberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                contact.SentTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

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
