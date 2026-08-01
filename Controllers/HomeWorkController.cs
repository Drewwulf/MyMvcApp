using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class HomeWorkController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private readonly IWebHostEnvironment _environment;

        public HomeWorkController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View(); // повертає Views/Homework/Index.cshtml
        }

        public IActionResult Details(int id)
        {
            var HomeworkPlace = _context.Homeworks.Find(id);
            var modal = new HomeworkViewModel
            {
                HomeworkName = HomeworkPlace.HomeworkName,
                HomeworkDescription = HomeworkPlace.HomeworkDescription,
                StartTime = HomeworkPlace.StartTime,
                SubmitTime = HomeworkPlace.SubmitTime,
                HomeworkId = HomeworkPlace.HomeworkId,
            };

            return View(modal); 
        }

        public IActionResult Info()
        {
            var allStudents = _context.Students.ToList();
            var allHomeWorks = _context.Homeworks.OrderByDescending(h => h.HomeworkId).ToList();
            var allStudentsToHomeWorks = _context.StudentsToHomeworks.OrderByDescending(s => s.StudentsToHomeworkId).ToList();

            var model = new HomeworkViewModel { homeworks = allHomeWorks, students = allStudents, studentsToHomeworks = allStudentsToHomeWorks };

            return View(model); // повертає Views/HomeWork/Info.cshtml
        }

        public IActionResult ViewHomeworkSumbit(int id)
        {
            var homeworkanswers = _context.HomeworkInfo.Where(h => h.HomeworkId == id).ToList();
            var HomeworkPlace = _context.Homeworks.Include(sh=>sh.StudentsToHomework).Where(h => h.HomeworkId == id).FirstOrDefault();
            var modal = new HomeworkViewModel
            {
                HomeworkName = HomeworkPlace.HomeworkName,
                HomeworkDescription = HomeworkPlace.HomeworkDescription,
                HomeworkInfos = homeworkanswers,
                HomeworkId = HomeworkPlace.HomeworkId,
                StudentToHomeWorkId = HomeworkPlace.StudentsToHomework.FirstOrDefault().StudentsToHomeworkId,
            };

            return View(modal);
        }

        public IActionResult DetailsStudentHomework(int id)
        {
            var homeworkanswers = _context.HomeworkInfo.Where(h => h.HomeworkId == id).ToList();
            var HomeworkPlace = _context.Homeworks.Include(sh => sh.StudentsToHomework).Where(h => h.HomeworkId == id).FirstOrDefault();
            var modal = new HomeworkViewModel
            {
                HomeworkName = HomeworkPlace.HomeworkName,
                HomeworkDescription = HomeworkPlace.HomeworkDescription,
                HomeworkInfos = homeworkanswers,
                HomeworkId = HomeworkPlace.HomeworkId,
                StudentToHomeWorkId = HomeworkPlace.StudentsToHomework.FirstOrDefault().StudentsToHomeworkId,
            };

            return View(modal);
        }

        public IActionResult Download(int id)
        {
            var homework = _context.HomeworkInfo.Where(h => h.HomeworkInfoId == id).FirstOrDefault();
            var document = _context.Documents.Where(d => d.Id == homework.DocumentId).FirstOrDefault();

            if (homework == null)
                return NotFound();

            var fullPath = Path.Combine(
                _environment.WebRootPath,
                document.FilePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString())
            );

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            return PhysicalFile(
                fullPath,
                "application/octet-stream",
                Path.GetFileName(fullPath));
        }

        [HttpPost]
        public async Task<IActionResult> Add(HomeworkViewModel model)
        {

            var allSH = _context.StudentsToHomeworks.Any(h => h.StudentId == model.StudentId && h.HomeworkId == model.HomeworkId);
            if (allSH) 
            {
                TempData["Error"] = "Ця домашння вже задана учневі";
                return RedirectToAction("Info");
            }


            var studentToHomeworkRecord = new StudentsToHomework
            {
                HomeworkId = model.HomeworkId,
                StudentId = model.StudentId,
                IsEnded = false
            };

            var student = _context.Students.Where(s => s.Id == model.StudentId).First();
            var studentAsUser = _context.Users.Where(u => u.Id == student.UserId).First();
            var emails = new EmailSender();
            await emails.SendEmailAsync(studentAsUser.NormalizedUserName, studentAsUser.Email, "Вам наначили нове домашнє завдання! School", "Вам призначили нове доманє завдання " + model.HomeworkName + ". Будь лакса виконайте до " + model.SubmitTime + "!");

            _context.StudentsToHomeworks.Add(studentToHomeworkRecord);
            _context.SaveChanges();
            return RedirectToAction("Info");

        }
        public IActionResult Create()
        {
             var allHomeworks = _context.Homeworks.OrderByDescending(h => h.HomeworkId).ToList();
            var model = new HomeworkViewModel{ homeworks = allHomeworks, };

            return View(model); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeworkViewModel model, IFormFile? file)
        {
            string? filePath = null;

            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "homeworks"); // Сохраняє файл в updloads/homeworks.

                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                var fullPath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                filePath = "/uploads/homeworks/" + fileName;
            }

            var homework = new Homework
            {
                HomeworkName = model.HomeworkName,
                HomeworkDescription = model.HomeworkDescription,
                StartTime = model.StartTime,
                SubmitTime = model.SubmitTime,
                FilePath = filePath
            };

            _context.Homeworks.Add(homework);
            await _context.SaveChangesAsync();

            

            return RedirectToAction("Create");
        }



        public IActionResult EndHomeWork(int id)
        {
            var studenttohomework = _context.StudentsToHomeworks.Where(s => s.StudentsToHomeworkId == id).FirstOrDefault();

            studenttohomework.IsEnded = true;
            _context.StudentsToHomeworks.Update(studenttohomework);
            _context.SaveChanges();

            return RedirectToAction("Info");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HomeworkViewModel homeworkViewModel)
        {
            var homework = new Homework
            {
                HomeworkName = homeworkViewModel.HomeworkName,
                HomeworkDescription = homeworkViewModel.HomeworkDescription
            };
            _context.Homeworks.Update(homework);
            _context.SaveChanges();
 var allHomeworks = _context.Homeworks.OrderByDescending(h => h.HomeworkId).ToList();
            var model = new HomeworkViewModel{ homeworks = allHomeworks};

            return RedirectToAction("Edit"); 
        }
         public IActionResult Edit(int id)
        
        {               
            var homeworkPlace =_context.Homeworks.Find(id);
            var modal = new HomeworkViewModel
            {
                HomeworkId = id,
                HomeworkName= homeworkPlace.HomeworkName,
                HomeworkDescription=homeworkPlace.HomeworkDescription

            };
            return View(modal); // повертає Views/Direction/Edit.cshtml
        }
        public IActionResult Delete(int id)
        {
            var homeworkPlace =_context.Homeworks.Find(id);
            homeworkPlace.isdeleted = true;
            _context.SaveChanges();
            return RedirectToAction("Create");
        }
    }
}