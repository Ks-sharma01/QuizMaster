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
            if(model ==null || model.UserAnswers == null)
            {
                return View("Error");
            }
            model.Score = 0;

            foreach (var answer in model.UserAnswers)
            {
                if(answer.Value == null)
                {
                    continue;
                }
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
