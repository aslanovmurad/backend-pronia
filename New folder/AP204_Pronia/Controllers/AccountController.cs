using AP204_Pronia.Models;
using AP204_Pronia.Utilities;
using AP204_Pronia.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP204_Pronia.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
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
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = new AppUser
            {
                UserName = registerVM.UserName,
                Lastname = registerVM.LastName,
                Email = registerVM.EmailAddress,
                Firstname = registerVM.FirstName
            };

            IdentityResult result = await userManager.CreateAsync(appUser, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await userManager.AddToRoleAsync(appUser, Roles.Member.ToString());
            await signInManager.SignInAsync(appUser, false);
            return RedirectToAction("Index", "Home");
        }
       
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {

            AppUser user = await userManager.FindByNameAsync(login.UserName);
            if (user == null) return View();
            IList<string> role = await userManager.GetRolesAsync(user);
            string roles = role.FirstOrDefault(r => r.ToLower().Trim() == Roles.Member.ToString().ToLower().Trim());
            if (roles == null) return View();

            if (login.RememberMe)
            {
                Microsoft.AspNetCore.Identity.SignInResult signIn = await signInManager.PasswordSignInAsync(user, login.Password, false, true);

                if (!signIn.Succeeded)
                {
                    ModelState.AddModelError("", "Username or password is incorrent");
                    return View();
                }
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult signIn = await signInManager.PasswordSignInAsync(user, login.Password, false, true);

                if (!signIn.Succeeded)
                {
                    if (signIn.IsLockedOut)
                    {
                        ModelState.AddModelError("", "you have been dismissed for 5 minutes");
                        return View();
                    }
                    ModelState.AddModelError("", "Username or password is incorrent");
                    return View();
                }
            }

            return RedirectToAction("index", "home");
        }
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();

            EditVM editVM = new EditVM
            {
                FirstName = user.Firstname,
                LastName = user.Lastname,
                EmailAddress = user.Email,
                UserName = user.UserName,
            };
            return View(editVM);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
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
