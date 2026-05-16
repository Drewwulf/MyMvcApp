using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public StudentController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
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
        public async Task<IActionResult> MyGroup()
        {
            var user = await _userManager.GetUserAsync(User);
            var userid = user.Id;
            var studentId = _context.Students.Where(s => s.UserId == userid).First().Id;
            var groups = _context.StudentToGroups
         .Include(g => g.studyGroup)
             .ThenInclude(sg => sg.Teachers)
         .Include(g => g.studyGroup)
             .ThenInclude(sg => sg.Place)
         .Include(g => g.student)
         .Where(g => g.StudentId == studentId)
         .ToList();
            var group = _context.StudentToGroups
        .Select(g => g.studyGroup)
        .Distinct()
        .ToList();
            var model = new StudentPageViewModel {groups = group };
            return View(model); // повертає Views/Destination/Details.cshtml
        }

        public async Task<IActionResult> Directions()
        {
            var user = await _userManager.GetUserAsync(User);
            var userid = user.Id;
            var studentId = _context.Students.Where(s => s.UserId == userid).First().Id;
            var groups = _context.StudentToGroups
         .ToList();
            var directionsID1 = _context.StudentToGroups
        .Select(g => g.studyGroup).Select(d => d.DirectionId)
        .Distinct()
        .ToList();
            var firstId = directionsID1.FirstOrDefault();

//            var direction = _context.Directions.FirstOrDefault(d => d.DirectionId == firstId);

            var directions = _context.Directions
    .Where(d => directionsID1.Contains(d.DirectionId))
    .ToList();

            return View(directions); // повертає /Views/Student/Directions.cshtml
        }
        public IActionResult MySchedule()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        } 

        public async Task<IActionResult> Homework()
        {
            var user = await _userManager.GetUserAsync(User);
            var userInfo = _context.Students.Where(i => i.UserId == user.Id).Include(hs => hs.StudentsToHomework).ThenInclude(h => h.Homework).ToList();

            return View(userInfo); // повертає Views/Student/Homework.cshtml
        }
    }
}