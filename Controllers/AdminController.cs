using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller

    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;


        public AdminController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _context=context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string FirstName, string LastName, string Email, string Role, string password)
        {
            // Формуємо повне ім'я
            var fullName = FirstName + "." + LastName;

            var emailList = _userManager.Users
        .Select(u => u.Email)
        .ToList();

            if (!emailList.Contains(Email))
            {

           

            // Створюємо нового користувача
            var user = new IdentityUser
            {
                UserName = fullName,
                Email = Email
            };
            if (Role == "Teacher")
            {
                var modal = new Teachers
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                _context.Teachers.Add(modal);
                _context.SaveChanges();

            }
            else if( Role ==  "Student")
            {
                var modal = new Students
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    StudyGroupId = null

                };
                _context.Students.Add(modal);
                _context.SaveChanges();
            }



            var result = await _userManager.CreateAsync(user, password); // пароль можна змінити

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
                UserName = user.UserName.Replace(".", " "),
                Role = roles.FirstOrDefault() ?? "No Role"
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUsers(UsersViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = model.Email;
            user.UserName = model.UserName.Replace(" ", "."); // Оновлюємо UserName, якщо він використовується як Email

            if (model.Role == "Teacher")
            {
                if (currentRoles.Contains("Student")) { _context.Students.Remove(_context.Students.Where(s => s.UserId == user.Id).First()); }
                _context.Teachers.Add(new Teachers { UserId = user.Id, UserName = user.UserName });
                _context.SaveChanges();

            }
            else if (model.Role == "Student")
            {
                if (currentRoles.Contains("Teacher")) { _context.Teachers.Remove(_context.Teachers.Where(s => s.UserId == user.Id).First()); }
                _context.Students.Add(new Students { UserId = user.Id, Username = user.UserName, StudyGroupId = null,  });
                _context.SaveChanges();
            }
            else if (model.Role == "Admin")
            {
                if (currentRoles.Contains("Teacher")) { _context.Teachers.Remove(_context.Teachers.Where(s => s.UserId == user.Id).First()); }
                if (currentRoles.Contains("Student")) { _context.Students.Remove(_context.Students.Where(s => s.UserId == user.Id).First()); }
                _context.SaveChanges();
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            if (!currentRoles.Contains(model.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            return RedirectToAction("TableUsers");
        }
    }
}

