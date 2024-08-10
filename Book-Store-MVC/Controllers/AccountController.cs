using Day2.Models;
using Day2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Day2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = userVM.Username,
                    Email = userVM.UserEmail,
                    PasswordHash = userVM.Password,
                    Address = userVM.Address,
                };
                
                IdentityResult result = await userManager.CreateAsync(user,userVM.Password);
                if (result.Succeeded)
                {
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    await userManager.AddToRoleAsync(user, "Customer");
                    return RedirectToAction("Login");
                }
                foreach (var item in result.Errors)
                    ModelState.AddModelError("", item.Description);
            }
            return View("Register",userVM);
        }

        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(userVM.UserEmail);
                if (user != null)
                {
                    var found = await userManager.CheckPasswordAsync(user,userVM.Password);

                    if (found)
                    {
                        await signInManager.SignInAsync(user, userVM.Remember_Me);
                        return RedirectToAction("Index", "Home");
                    }
                }       
            }
            ModelState.AddModelError("", "Invalid Data Login !");
            return View("Login", userVM);
        }
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }

    
}

