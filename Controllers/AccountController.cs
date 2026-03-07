using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(
    SignInManager<IdentityUser> signInManager,
    UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
        
            if(User.IsInRole("Teacher"))
                return RedirectToAction("Index", "Home");
            if (User.IsInRole("Student"))
                return RedirectToAction("Index", "Home");
            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                ViewBag.Error = "Користувач не знайдений";
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                password,
                false,
                false);

            if (!result.Succeeded)
            {
                ViewBag.Error = "Неправильний пароль";
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
