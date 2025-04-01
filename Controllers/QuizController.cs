using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                        Answers = q.Answers
                    }).ToList()

            };
            TempData["Count"] = viewModel.Questions.Count;
            return View(viewModel);

        }

        [Authorize]
        [HttpPost]
        public IActionResult SubmitAnswer(QuizViewModel model)
        {
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

//            [HttpPost]
//            public IActionResult SubmitAnswer(int CurrentQuestionIndex, int SelectedAnswer)
//        {
//            // Get the list of questions from the session or database
//            var questions = HttpContext.Session.GetObject<List<Question>>("QuizQuestions");
//            var viewModel = new QuizViewModel
//            {
//                Questions = questions,
//                CurrentQuestionIndex = CurrentQuestionIndex,
//                Score = HttpContext.Session.GetInt32("QuizScore") ?? 0
//            };

//            // Check if the selected answer is correct
//            if (questions[CurrentQuestionIndex].CorrectAnswerId == SelectedAnswer)
//            {
//                viewModel.Score++;
//            }

//            // Save the updated score in the session
//            HttpContext.Session.SetInt32("QuizScore", viewModel.Score);

//            // Move to the next question or display the result
//            if (CurrentQuestionIndex >= questions.Count - 1)
//            {
//                // Final result
//                return View("Result", viewModel.Score);
//            }

//            viewModel.CurrentQuestionIndex++;
//            return View("Quiz", viewModel);
//        }

//    }
//}
