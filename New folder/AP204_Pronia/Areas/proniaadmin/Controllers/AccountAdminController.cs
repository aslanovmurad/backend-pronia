using AP204_Pronia.Models;
using AP204_Pronia.Utilities;
using AP204_Pronia.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP204_Pronia.Areas.proniaadmin.Controllers
{
    [Area("proniaadmin")]

    public class AccountAdminController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountAdminController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = new AppUser
            {
                UserName = register.UserName,
                Lastname = register.LastName,
                Email = register.EmailAddress,
                Firstname = register.FirstName
            };

            IdentityResult result = await userManager.CreateAsync(appUser, register.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            await userManager.AddToRoleAsync(appUser, Roles.SurperAdmin.ToString());
            await signInManager.SignInAsync(appUser, false);
            return RedirectToAction("Index", "Dashboard");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser appUser = await userManager.FindByNameAsync(login.UserName);
            if (appUser == null) return View();

            IList<string> list = await userManager.GetRolesAsync(appUser);
            string role = list.FirstOrDefault(r => r.ToLower().Trim() == Roles.SurperAdmin.ToString().ToLower().Trim());
            if (role == null) return View();

            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is incorrent");
                return View();
            }
            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Dashboard");

        }
        public IActionResult Show()
        {
            return Content(User.Identity.IsAuthenticated.ToString());
        }
        public async Task CreateRoles()
        {
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Member.ToString() });
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() });
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.SurperAdmin.ToString() });
        }
    }
}
