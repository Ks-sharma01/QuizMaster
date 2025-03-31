using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize]
        public IActionResult Index()
        {
            var viewModel = new QuizViewModel
            {
                Questions = _context.Questions
                    .Select(q => new Question
                    {
                        QuestionId = q.QuestionId,
                        Text = q.Text,
                        Answers = q.Answers.ToList()
                    }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SubmitAnswer(QuizViewModel model)
        {
            if(model == null)
            {
                ModelState.AddModelError("Error", "Answer atleast one question");

            }

            model.Score = 0;

            foreach (var answer in model.UserAnswers)
            {
                
                var correctAnswer = _context.Answers.FirstOrDefault(a => a.AnswerId == answer.Value && a.IsCorrect);
                if (correctAnswer != null)
                {
                    model.Score++;
                }
            }
            
            return View("Result", model);
        }
    }
}
