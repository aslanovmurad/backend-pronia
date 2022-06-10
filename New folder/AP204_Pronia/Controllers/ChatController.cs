using AP204_Pronia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AP204_Pronia.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public ChatController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await userManager.Users.ToListAsync();
            return View(users);
        }
    }
}
