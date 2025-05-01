using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuizDishtv.Data;
using QuizDishtv.Models;
using QuizDishtv.ViewModels;
using System.Data;
using System.Linq;

namespace QuizDishtv.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly QuizDbContext _context;
        //private readonly IQuizService _quizService;

        public AdminController(QuizDbContext context)
        {
            _context = context;
            //_quizService = quizService;
        }

        public IActionResult Leaderboard(int categoryId)
        {
            var LeaderboardData = _context.Results.Where(x => x.CategoryId == categoryId)
                .OrderByDescending(s => s.Score)
                .Include(s => s.User)
                .Include(s => s.Category).ToList();
            var viewModel = LeaderboardData.Select((s, index) => new LeaderboardViewModel
            {
                Rank = index + 1,
                Username = s.User.UserName,
                CategoryName = s.Category.Name,
                Score = s.Score,
            }).ToList();
            return Json(viewModel);
        }

        public IActionResult SelectCategory()
        {
            var categories = _context.Category.ToList();
            return View(categories);
        }

        public IActionResult AssignRole()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public IActionResult AssignRole(int userId, string role)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                user.Role = role;
                _context.Update(user);
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard", "Admin");
        }
        //public IActionResult AddQuestion()
        //{
        //    return View(new QuestionInputViewModel 
        //    { 
        //Answers = new List<AnswerInputViewModel> 
        //      { 
        //        new AnswerInputViewModel()
        //      } 
        //    });
        //}
        
        public async Task<IActionResult> Questions()
        {
            //var rawData = await _context.Set<QuestionAnswerDto>()
            //    .FromSqlRaw("EXEC GetAllQuestionsWithAnswers")
            //    .ToListAsync();

            //var groupedQuestions = rawData
            //    .GroupBy(q => new { q.QuestionId, q.QuestionText, q.CategoryId, q.CategoryName })
            //    .Select(g => new FetchQuestionsViewModel
            //    {
            //        QuestionId = g.Key.QuestionId,
            //        QuestionText = g.Key.QuestionText,
            //        CategoryId = g.Key.CategoryId,
            //        CategoryName = g.Key.CategoryName.Distinct().ToString(),
            //        Options = g.Select(a => new FetchOptionsViewModel
            //        {
            //            AnswerId = a.AnswerId,
            //            AnswerText = a.AnswerText,
            //            IsCorrect = a.IsCorrect
            //        }).ToList()
            //    }).ToList();

            //return View(groupedQuestions); 
            var categories = await _context.Category.ToListAsync();

            var questionWithCategories = await _context.Questions
                .Include(q => q.Category)
                .Include(q => q.Answers).ToListAsync();

            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View(questionWithCategories);
        }

        public async Task<IActionResult> AddQuestion()
        {
            var categories = await _context.Category.ToListAsync();
            var viewModel = new QuestionInputViewModel
            {
                CategoryList = new SelectList(categories, "CategoryId", "Name"),
                Answers = new List<AnswerInputViewModel>
              {
                new AnswerInputViewModel()
              }
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuestion(QuestionInputViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var categories = await _context.Category.ToListAsync();
            //    model.CategoryList = new SelectList(categories, "CategoryId", "Name");
            //    return View(model);
            //}
            //    await _quizService.AddQuestionAsync(model);
            //    return RedirectToAction("AddQuestion");
            ////return View(model);
            ///

            //if (!ModelState.IsValid)
            //{
            //    // Re-populate categories in case of error
            //    var categories = await _context.Category.ToListAsync();
            //    model.CategoryList = new SelectList(categories, "CategoryId", "Name");
            //    return View(model);
            //}

            //// Insert the question using the stored procedure
            //var parameters = new[]
            //{
            //     new SqlParameter("@Text", model.Text),
            //     new SqlParameter("@CategoryId", model.CategoryId)
            // };

            //await _context.Database.ExecuteSqlRawAsync("EXEC AddQuestion @Text, @CategoryId", parameters);

            //// Get the inserted question (by text match and descending ID)
            //var insertedQuestion = await _context.Questions
            //    .OrderByDescending(q => q.QuestionId)
            //    .FirstOrDefaultAsync(q => q.Text == model.Text);

            //// Insert each answer for the question
            //if (insertedQuestion != null)
            //{
            //    foreach (var answer in model.Answers)
            //    {
            //        await _context.Database.ExecuteSqlRawAsync(
            //            "EXEC AddAnswer @QuestionId = {0}, @Text = {1}, @IsCorrect = {2}",
            //            insertedQuestion.QuestionId, answer.Text, answer.IsCorrect
            //        );
            //    }
            //}

            //return RedirectToAction("AddQuestion");

            // Server-side validation
            //if (!model.Answers.Any(a => a.IsCorrect))
            //{
            //    ModelState.AddModelError(string.Empty, "At least one answer must be marked as correct.");
            //}

            //for (int i = 0; i < model.Answers.Count; i++)
            //{
            //    if (string.IsNullOrWhiteSpace(model.Answers[i].Text))
            //    {
            //        ModelState.AddModelError($"Answers[{i}].Text", $"Option {i + 1} is required.");
            //    }
            //}

            //if (!ModelState.IsValid)
            //{
            //    var categories = await _context.Category.ToListAsync();
            //    model.CategoryList = new SelectList(categories, "CategoryId", "Name");
            //    return View(model);
            //}

            // Insert Question via Stored Procedure
            var parameters = new[]
            {
            new SqlParameter("@Text", model.Text),
            new SqlParameter("@CategoryId", model.CategoryId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC AddQuestion @Text, @CategoryId", parameters);

            // Get the inserted question to get its ID
            var insertedQuestion = await _context.Questions
                .OrderByDescending(q => q.QuestionId)
                .FirstOrDefaultAsync(q => q.Text == model.Text);

            if (insertedQuestion != null)
            {
                foreach (var answer in model.Answers)
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC AddAnswer @QuestionId = {0}, @Text = {1}, @IsCorrect = {2}",
                        insertedQuestion.QuestionId, answer.Text, answer.IsCorrect
                    );
                }
            }

            return RedirectToAction("Questions"); // Redirect to the question list
        }

        public async Task<IActionResult> EditQuestion(int id)
        {
            var question = await _context.Questions.Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuestionId == id);
            if(question == null)
            {
                return NotFound();
            }
            var categories = await _context.Category.ToListAsync();
            var viewModel = new EditQuestionViewModel
            {
                QuestionId = question.QuestionId,
                Text = question.Text,
                CategoryId = question.CategoryId,
                editAnswers = question.Answers.Select(x => new EditAnswerViewModel
                {
                    AnswerId = x.AnswerId,
                    Text = x.Text,
                    IsCorrect = x.IsCorrect,
                }).ToList(),
                 CategoryList = new SelectList(categories, "CategoryId", "Name")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuestion(EditQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            var question = await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuestionId==model.QuestionId);

            if(question == null)
            {
                return NotFound();
            }
            question.Text = model.Text;
            question.CategoryId = model.CategoryId;

            foreach(var answermodel in model.editAnswers)
            {
                var answer = question.Answers.FirstOrDefault(a => a.AnswerId==answermodel.AnswerId);
                if(answer != null)
                {
                    answer.Text = answermodel.Text;
                    answer.IsCorrect = answermodel.IsCorrect;
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Questions", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuestionId == id);

            if (question == null)
                return NotFound();

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return RedirectToAction("Questions", "Admin");
        }



    }
}
