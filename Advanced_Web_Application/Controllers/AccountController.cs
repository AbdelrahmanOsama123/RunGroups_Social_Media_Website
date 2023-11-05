using Advanced_Web_Application.Data;
using Advanced_Web_Application.Models;
using Advanced_Web_Application.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Advanced_Web_Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await signInManager.SignOutAsync();
            ViewData["LoginCSS"] = true;
            var LoginModel = new LoginViewModel();
            return View(LoginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel LoginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(LoginVM);
            }

            var user = await userManager.FindByEmailAsync(LoginVM.EmailAddress);
            if (user != null) {
                var passwordCheck = await userManager.CheckPasswordAsync(user, LoginVM.Password);
                if (passwordCheck)
                {
                    var SignInResult = await signInManager.PasswordSignInAsync(user, LoginVM.Password, false, false);
                    if (!SignInResult.Succeeded)
                    {
                        ModelState.AddModelError("Password", "SignIn Failed");
                        TempData["Error"] = "Wrong Credential , Please Try Again";
                        return View(LoginVM);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["Error"] = "Wrong Password, Please Try Again";
                    return View(LoginVM);
                }
            }
            else
            {
                TempData["Error"] = "Wrong Username, Please Try Again";
                return View(LoginVM);
            }
        }

       
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["LoginCSS"] = true;
            var RegisterModel = new RegisterViewModel();
            return View(RegisterModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel RegisterVM)
        {
            if (!ModelState.IsValid)
            {
                return View(RegisterVM);
            }

            var userEmail = await userManager.FindByEmailAsync(RegisterVM.EmailAddress);
            var userName = await userManager.FindByNameAsync(RegisterVM.Username);
            if (userEmail != null)
            {
                TempData["Error"] = "Email Address is taken before , Try another one";
                return View(RegisterVM);
            }

            if (userName != null)
            {
                TempData["Error"] = "Username is taken before , Try another one";
                return View(RegisterVM);
            }
            var newUser = new AppUser
            {
                FirstName = RegisterVM.FirstName,
                LastName = RegisterVM.LastName,
                UserName = RegisterVM.Username,
                Email = RegisterVM.EmailAddress
            };

            var newUserResult = await userManager.CreateAsync(newUser, RegisterVM.Password);
            if (newUserResult.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, UserRoles.User);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["Error"] = "Password must be at least special Character,UpperCase,LowerCase and Six Characters Long";
                return View(RegisterVM);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
    }
}
