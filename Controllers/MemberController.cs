using BetaCinemas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetaCinemas.Controllers
{
    public class MemberController : Controller
    {
        private readonly UserManager<Member> userManager;

        public MemberController(UserManager<Member> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(userManager.Users.ToListAsync());
        }
    }
}
