using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;
using static System.Formats.Asn1.AsnWriter;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private Students student;

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
            var model = new StudentPageViewModel { groups = group };
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

        public IActionResult Tests(int id)
        {
            var allTests = _context.Tests.Where(ts => ts.DirectionId == id).ToList();
            return View(allTests);
        }

        public IActionResult TestPage(int id)
        {
            var tasks = _context.Tasks.Where(ts => ts.TestId == id).Include(t => t.Test).Include(ans => ans.Answers).ToList();


            return View(tasks);
        }

        [HttpGet]
        public IActionResult ShowScore(int score, int total)
        {
            double percentage = total > 0 ? (double)score / total * 100 : 0;

            var viewModel = new ScoreViewModel
            {
                Score = score,
                TotalQuestions = total,
                Percentage = Math.Round(percentage, 1) 
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckTest(List<AnswerSubmission> submissions)
        {
            int correctAnswersCount = 0;
            int totalQuestions = submissions.Count;

            var user = await _userManager.GetUserAsync(User);
            var userid = user.Id;
            var studentId = _context.Students.Where(s => s.UserId == userid).First().Id;
            foreach (var submission in submissions)
            {
                var question = _context.Tasks
                                       .Include(q => q.Answers)
                                       .FirstOrDefault(q => q.QuestionId == submission.QuestionId);

                if (question == null) continue;
                if (question.QuestionType == "CheckBox") 
                {
                    var correctAnswersIds = question.Answers
                                                    .Where(a => a.IsCorrect)
                                                    .Select(a => a.Id)
                                                    .ToList();

                    var userSelectedIds = submission.SelectedAnswerId ?? new List<int>();

                    bool isAllCorrect = correctAnswersIds.Count == userSelectedIds.Count &&
                                        userSelectedIds.All(id => correctAnswersIds.Contains(id));

                    if (isAllCorrect)
                    {
                        correctAnswersCount++;
                    }
                }
                else 
                {
                    var firstSelectedId = submission.SelectedAnswerId?.FirstOrDefault();

                    var selectedAnswer = question.Answers
                                                 .FirstOrDefault(a => a.Id == firstSelectedId);

                    if (selectedAnswer != null && selectedAnswer.IsCorrect)
                    {
                        correctAnswersCount++;
                    }
                }
            }
            var gett = new ResultTest { Score = correctAnswersCount, TestId = _context.Tasks.Include(q => q.Answers).FirstOrDefault(q => q.QuestionId == submissions.First().QuestionId).TestId,StudentId = studentId, DateTime = DateTime.Now };
            _context.ResultsTests.Add(gett);
            _context.SaveChanges();
            return RedirectToAction("ShowScore", new { score = correctAnswersCount, total = totalQuestions });
        }

        public async Task<IActionResult> MySchedule()
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
            var group = groups
        .Select(g => g.studyGroup)
        .Distinct()
        .ToList();
            var schedules = _context.Schedules.Where(g => g.StudyGroupId == group.First().StudyGroupId).Include(sg => sg.Place).ToList();
            var viewModel = new SheduleViewModel
            {
                Schedules = schedules

            };
            return View(viewModel); // повертає Views/Destination/Details.cshtml
        }


        public async Task<IActionResult> Homework()
        {
            var user = await _userManager.GetUserAsync(User);
            var userInfo = _context.Students.Where(i => i.UserId == user.Id).Include(hs => hs.StudentsToHomework).ThenInclude(h => h.Homework).ToList();

            return View(userInfo); // повертає Views/Student/Homework.cshtml
        }
    }
}