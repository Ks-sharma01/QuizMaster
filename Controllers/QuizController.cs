using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuizDishtv.Data;
using QuizDishtv.Models;
using QuizDishtv.ViewModels;
using System.Linq;

namespace QuizDishtv.Controllers
{
    [Authorize(Roles = "User")]
    public class QuizController : Controller
    {
        private readonly QuizDbContext _context;

        public QuizController(QuizDbContext context)
        {
            _context = context;
        }

        public IActionResult Categories()
        {
            var categories = _context.Category.ToList();
            return View(categories);
        }

        public IActionResult StartQuiz(int categoryId, int questionIndex = 0)
        {
            //    var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserId;
            //    //Get all the questions for the selected category

            var questions = _context.Questions
                                    .Where(q => q.CategoryId == categoryId)
                                    .Include(q => q.Answers)  // Including answers if needed
                                    .ToList();

            //    var questions = _context.Questions
            //.FromSqlRaw(@"
            //    SELECT TOP 1 * 
            //    FROM Questions 
            //    WHERE CategoryId = {0} 
            //      AND Id NOT IN (
            //          SELECT QuestionId FROM AttemptedQuestions WHERE UserId = {1}
            //      ) 
            //    ORDER BY NEWID()", categoryId, userId)
            //.FirstOrDefaultAsync();

            //// Check if there are any questions available
            if (questions.Count == 0)
            {
                return NotFound("No questions found for this category.");
            }

            if (questionIndex >= questions.Count)
            {
                return RedirectToAction("ShowResult", new { categoryId });
            }

            var question = questions[questionIndex];
            ViewBag.CategoryId = categoryId;

            ViewBag.QuestionIndex = questionIndex;
            ViewBag.TotalQuestions = questions.Count;

            ////return View(nextQuestion);
            return View(question);



            //var questions = _context.Questions
            //                      .Where(q => q.CategoryId == categoryId)
            //                       .Include(q => q.Answers)
            //                       .ToList();

            //// Get current user ID
            //int userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserId;
            //if (userId == null)
            //{
            //    return Unauthorized();
            //}
            //var parameters = new[]
            //{
            //    new SqlParameter("@CategoryId", categoryId),
            //    new SqlParameter("@UserId", userId)
            //};
            //// Fetch one random unattempted question from database
            //var question =  _context.Questions
            //    .FromSqlRaw("EXEC spGetRandomQuestion @CategoryId, @UserId",parameters ).AsEnumerable()
            //    .FirstOrDefault();

            //if (question == null)
            //{
            //    return RedirectToAction("ShowResult", new { categoryId });
            //}

            //var alreadyAttempted =  _context.AttemptedQuestions
            //    .Any(a => a.UserId == userId && a.QuestionId == question.QuestionId);

            //if (!alreadyAttempted)
            //{
            //    _context.AttemptedQuestions.Add(new AttemptedQuestion
            //    {
            //        UserId = userId,
            //        CategoryId = categoryId,
            //        QuestionId = question.QuestionId,
            //        AttemptedOn = DateTime.Now
            //    });
            //     _context.SaveChanges();
            //}
            //ViewBag.QuestionIndex = questionIndex;
            //ViewBag.TotalQuestions = questions.Count;

            //ViewBag.CategoryId = categoryId;
            //return View(question);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SubmitAnswer(int categoryId, int questionId, int selectedAnswerId, int questionIndex)   //int categoryId, int questionId, int selectedAnswerId,
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
            ViewBag.CategoryId = categoryId;

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
            ViewBag.CategoryId = categoryId;

            return View(answers);

        }

        public IActionResult RestartQuiz(int categoryId)
        {
            var userId = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name)?.UserId;
            if(userId == null)
            {
                return Unauthorized();
            }

            var attemptsToRemove = _context.AttemptedQuestions
                .Where(a => a.UserId == userId && a.Question.CategoryId == categoryId).ToList();
            _context.AttemptedQuestions.RemoveRange(attemptsToRemove);
            _context.SaveChanges();

            return RedirectToAction("Categories");
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
