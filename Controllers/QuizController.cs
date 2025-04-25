using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using QuizDishtv.Data;
using QuizDishtv.Models;

namespace QuizDishtv.Controllers
{
    public class QuizController : Controller
    {
        private readonly QuizDbContext _context;

        public QuizController(QuizDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles ="User")]
        public IActionResult Categories()
        {
            var categories = _context.Category.ToList();
            return View(categories);
        }

        [Authorize]
        public IActionResult StartQuiz(int categoryId, int questionIndex = 0)
        {
            var questions = _context.Questions
                .Include(q => q.Answers)
                .Where(q => q.CategoryId == categoryId).ToList();
            if (questionIndex >= questions.Count)
            {
                return RedirectToAction("ShowResult", new { categoryId });
            }

            var question = questions[questionIndex];
            ViewBag.CategoryId = categoryId;
            ViewBag.QuestionIndex = questionIndex;
            ViewBag.TotalQuestions = questions.Count;

            return View(question);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SubmitAnswer(int categoryId, int questionId, int selectedAnswerId, int questionIndex)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserId;
            var existing = _context.UserAnswer.Where(x => x.UserId == userId && x.QuestionId == questionId).FirstOrDefault();

            if (existing == null)
            {
                existing = new UserAnswer
                {
                    UserId = userId,
                    QuestionId = questionId,
                    SelectedAnswerId = selectedAnswerId,
                    CategoryId = categoryId
                };
                _context.UserAnswer.Add(existing);
                _context.SaveChanges();
            }
            else
            {
                existing.SelectedAnswerId = selectedAnswerId;
                _context.UserAnswer.Update(existing);
            }
            _context.SaveChanges();
            return RedirectToAction("StartQuiz", new { categoryId, questionIndex = questionIndex + 1 });
        }

        public IActionResult ShowResult(int categoryId)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserId;
            var answers = _context.UserAnswer
                .Include(ua => ua.SelectedAnswer)
                .Include(ua => ua.Question)
                .ThenInclude(ua => ua.Answers)
                .Include(ua => ua.Category)
                .Where(ua => ua.UserId == userId && ua.Question.CategoryId == categoryId)
                .ToList();
            int score = answers.Count(a => a.SelectedAnswer.IsCorrect);
            // Check if result already exists
            var result = _context.Results
                .FirstOrDefault(x => x.CategoryId == categoryId && x.UserId == userId);

            if (result == null)
            {
                result = new Result
                {
                    Score = score,
                    CategoryId = categoryId,
                    UserId = userId,
                    AttemptedOn = DateTime.Now
                };
                _context.Results.Add(result);
            }
            else
            {
                result.Score = score;
                result.AttemptedOn = DateTime.Now;
                _context.Results.Update(result);
            }
            _context.SaveChanges();

            ViewBag.Score = score;
            TempData["Total"] = answers.Count;

            return View(answers);
        }

        [Authorize]
        public IActionResult SelectQuiz()
        {
            var subject = _context.Category.Select(p => new { p.CategoryId, p.Name });
            return View(subject);
        }

        public IActionResult Profile()
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserId;

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            var quizAttempts = _context.Results.Include(a => a.Category)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.AttemptedOn).ToList();
            if (quizAttempts == null)
            {
                return NotFound("No Quiz Attempts");
            }

            var viewModel = new ResultViewModel
            {
                Username = user.UserName,
                Email = user.UserEmail,
                QuizScores = quizAttempts.Select(a => new QuizScoreDto
                {
                    QuizTitle = a.Category.Name,
                    Score = a.Score,
                    AttemptedOn = a.AttemptedOn,

                }).ToList()
            };

            return View(viewModel);

        }
    
    }
}
