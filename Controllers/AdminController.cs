using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models;

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
public async Task<IActionResult> CreateUser(string FirstName, string LastName, string Email, string Role,string password)
{
    // Формуємо повне ім'я
    var fullName = FirstName + "." + LastName;

    // Створюємо нового користувача
    var user = new IdentityUser
    {
        UserName = fullName,
        Email = Email
    };

    var result = await _userManager.CreateAsync(user, password ); // пароль можна змінити

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
                    Id = user.Id,
                    UserName = user.UserName,          
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "No Role"
                });

            }
            return View(result);
        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("TableUsers");
        }
        public async Task<IActionResult> EditUsers(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var model = new UsersViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName.Replace("."," "),
                Role = roles.FirstOrDefault() ?? "No Role"
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUsers(UsersViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = model.Email;
            user.UserName = model.UserName.Replace(" ","."); // Оновлюємо UserName, якщо він використовується як Email

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(model.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            return RedirectToAction("TableUsers");
    }}
} 

