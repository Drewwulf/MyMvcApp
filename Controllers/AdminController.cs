using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
public async Task<IActionResult> CreateUser(string FirstName, string LastName, string Email, string Role)
{
    // Формуємо повне ім'я
    var fullName = FirstName + " " + LastName;

    // Створюємо нового користувача
    var user = new IdentityUser
    {
        UserName = Email,
        Email = Email
    };

    var result = await _userManager.CreateAsync(user, "DefaultPassword123!"); // пароль можна змінити

    if (result.Succeeded)
    {
        // Додаємо роль
        await _userManager.AddToRoleAsync(user, Role);

        ViewBag.Success = "Користувача створено!";
        return View();
    }

    // Якщо є помилки
    foreach (var error in result.Errors)
    {
        ModelState.AddModelError("", error.Description);
    }

    return View();
}


        [HttpGet]
        public async Task<IActionResult> TableUsers()
        { 
           var users = await _userManager.Users.OrderBy(u => u.UserName).ToListAsync();

            var result = new List<UsersViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UsersViewModel
                {
                    Id = user.UserName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "No Role"
                });
            }
            return View(result);
        }
    
    }
}
