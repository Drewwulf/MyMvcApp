using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public StudentController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var studentslist = _context.Students.ToList();

            return View(studentslist); // повертає Views/Student/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Student/Edit.cshtml
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
         public IActionResult MyGroup()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
        public IActionResult MySchedule()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
    }
}