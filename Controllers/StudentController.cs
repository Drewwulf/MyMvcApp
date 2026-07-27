using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;
using System.Runtime.InteropServices;
using static System.Formats.Asn1.AsnWriter;

namespace MyMvcApp.Controllers
{
     [Authorize(Roles = "Teacher")]
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

        private async Task<int> GetStudentId()
        {
            var user = await _userManager.GetUserAsync(User);

            var studentId = _context.Students
                .First(s => s.UserId == user.Id)
                .Id;

            return studentId;
        }

        private async Task<List<StudentToGroup>> GetStudentsGroup()
        {
            var studentId = await GetStudentId();
            var groups = _context.StudentToGroups
         .Include(g => g.studyGroup)
             .ThenInclude(sg => sg.Teachers)
         .Include(g => g.studyGroup)
             .ThenInclude(sg => sg.Place).Include(g => g.studyGroup)
             .ThenInclude(sg => sg.Direction)
             .Include(g => g.studyGroup).ThenInclude(sg => sg.Schedule)
         .Include(g => g.student)
         .Where(g => g.StudentId == (int)studentId)
         .ToList();

            return groups;
        }

        private async Task<int> GetStudentPoints()
        {
            int studentId = await GetStudentId();
            var studentLevel = _context.Students.Where(s => s.Id == studentId).Select(l => l.StudentPoints).FirstOrDefault();
            return studentLevel;
        }

        public async Task<int> GetStudentLevel()
        {
            int studentPoints = await GetStudentPoints();

            return studentPoints switch // 1 - 
            {
                >= 1 and <= 10 => 1,
                > 10 and <= 25 => 2,
                > 25 and <= 50 => 3,
                > 50 => 4 + (studentPoints - 51) / 50,
                _ => 1
            };
        }
        private async Task<List<Direction>> GetStudentsDirection()
        {
            var studentId = await GetStudentId();
            var groups = await GetStudentsGroup();
            var directions = new List<Direction>();
            var directions2 = groups.Select(g => g.studyGroup.Direction).ToList();


            if (groups != null && groups.Count > 0 && groups[0].studyGroup != null)
            {
                foreach (var Did in groups)
                {
                    directions.Append(Did.studyGroup.Direction);
                }
            }

            return directions2;
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
        public async Task<IActionResult> StudentKabinet()
        {
            var group = await GetStudentsGroup();
            var dir = await GetStudentsDirection();
            var user = await _userManager.GetUserAsync(User);
            
            var model = new StudentPageViewModel { directions = dir,allGroups = group,user  =  user.UserName,email = user.Email};
            return View(model);
           
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
        public async Task<IActionResult> MyGroup()
        {
            var group = await GetStudentsGroup();
            var model = new StudentPageViewModel { allGroups = group };
            return View(model);
        }

        public async Task<IActionResult> Directions()
        {
            var directions = await GetStudentsDirection();

            var studentLevel = await GetStudentLevel();
            var studentPoints = await GetStudentPoints();

            var model = new StudentDirectionViewModel
            {
                directions = directions,
                StudentLevel = studentLevel,
                StudentPoints = studentPoints
            };

            return View(model);
        }

        public IActionResult Tests(int id)
        {
            var allTests = _context.Tests.Where(ts => ts.DirectionId == id).Where(q => q.Questions.Any()).Where(q => q.Questions.Any(a => a.Answers.Any())).ToList();
            return View(allTests);
        }

        public IActionResult TestPage(int id)
        {
            var tasks = _context.Tasks.Where(ts => ts.TestId == id).Include(t => t.Test).Include(ans => ans.Answers).Where(ts => ts.isdeleted != true).ToList();
            var bb = tasks.FirstOrDefault().Answers.Any(a => a.IsCorrect == true);

            return View(tasks);
        }

        [HttpGet] public async Task<IActionResult> ShowScore(double score, int total, TestDifficualtyEnum TestDifficualty) { double percentage = total > 0 ? (double)score / total * 100 : 0; var viewModel = new ScoreViewModel { Score = score, TotalQuestions = total, Percentage = Math.Round(percentage, 1) }; int userid = await GetStudentId(); var user = _context.Students.Where(s => s.Id == userid).FirstOrDefault(); if (user != null) { if (percentage != 60) { user.StudentPoints += Convert.ToInt32(Math.Round((int)TestDifficualty * (percentage / 100.0))); _context.SaveChanges(); } } return View(viewModel); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckTest(List<AnswerSubmission> submissions)
        {
            decimal correctAnswersCount = 0;
            int totalQuestions = submissions.Count;

            var studentId = await GetStudentId();
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


                    if (userSelectedIds.Any())
                    {
                        decimal matchedAnswerscoof = (decimal)1 / correctAnswersIds.Count;
                        var matchedAnswersCount = matchedAnswerscoof * userSelectedIds.Count(id => correctAnswersIds.Contains(id));
                        correctAnswersCount += matchedAnswersCount;
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
            var gett = new ResultTest { Score = Math.Round(totalQuestions > 0 ? correctAnswersCount / totalQuestions * 100 : 0, 1), TestId = _context.Tasks.Include(q => q.Answers).FirstOrDefault(q => q.QuestionId == submissions.First().QuestionId).TestId, StudentId = studentId, DateTime = DateTime.Now, TestDifficualty = _context.Tasks.Include(q => q.Test).FirstOrDefault().Test.TestDifficualty };
            _context.ResultsTests.Add(gett);
            _context.SaveChanges();
            var tt = _context.Tasks.Include(q => q.Test).FirstOrDefault().Test.TestDifficualty;
            return RedirectToAction("ShowScore", new { score = correctAnswersCount, total = totalQuestions, TestDifficualty = tt });
            // return RedirectToAction("ShowScore", new ScoreRequest { Score = correctAnswersCount, Total = totalQuestions, TestDifficualty = tt });
        }



        public async Task<IActionResult> HistoryTest()
        {
            var studentId = await GetStudentId();
            var scoree = _context.ResultsTests.Where(s => s.StudentId == studentId).Include(t => t.Test).ToList();


            return View(scoree);
        }


        public async Task<IActionResult> MySchedule()
        {
            var group = await GetStudentsGroup();
            var schedules = group.SelectMany(gs => gs.studyGroup.Schedule).ToList();

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
        public async Task<IActionResult> LiderBoard()
        {
            var students = _context.Students.OrderByDescending(s => s.StudentPoints).ToList();
            return View(students); // повертає Views/Student/HomeworkDetails.cshtml
        }
    }
}