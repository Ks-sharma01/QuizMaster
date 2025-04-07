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
        public IActionResult Index(QuizViewModel model)
        {
            var viewModel = new QuizViewModel
            {
            Questions = _context.Questions
            .Include(q => q.Answers)
            .Select(p => new Question
            {
                QuestionId = p.QuestionId,
                Text = p.Text,
                Answers = p.Answers
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
            TempData["Score"] = model.Score;
            //HttpContext.Session.SetString("UserAnswers", System.Text.Json.JsonSerializer.Serialize(model.UserAnswers));
            HttpContext.Session.SetString("UserAnswers", JsonConvert.SerializeObject(model.UserAnswers));
            //HttpContext.Session.SetInt32("Score", model.Score);
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.UserId;
            var QuizResult = _context.Results.FirstOrDefault(temp => temp.UserId == userId);

            if (QuizResult != null)
            {
                QuizResult.Score=model.Score;
                
            }
            else
            {
                QuizResult = new Result
                {
                    Score = model.Score,
                    UserId = userId.Value,
                };
                _context.Results.Add(QuizResult);
            }
            _context.SaveChanges();
            //if(userId != null)
            //{
            //    var result = new Result
            //    {
            //        Score = model.Score,
            //        UserId = userId.Value,

            //    };
            //    _context.Results.Add(result);
            //    _context.SaveChanges();

            //var viewModel = new QuizViewModel
            //{
            //    UserAnswers = model.UserAnswers,
            //};
            //}   
            return View("Result", model);

        }

        //public IActionResult UpdateScore(int userId, int score)
        //{
        //    var QuizResult = _context.Results.FirstOrDefault(temp => temp.UserId == userId);
        //    if(QuizResult != null)
        //    {
        //        QuizResult.Score = score;

        //    }
        //    else
        //    {
        //        QuizResult = new Result
        //        {
        //            UserId = userId,
        //            Score = score
        //        };
        //        _context.Results.Add(QuizResult);
        //    }
        //    _context.SaveChanges();
        //    return View(QuizResult);
        //}
        //public IActionResult Result(QuizViewModel model)
        //{
        //    //var result = _context.Results.FirstOrDefault(temp => temp.UserId == model.Users.UserId);
        //    var viewModel = new QuizViewModel
        //    {
        //        Questions = model.Questions.ToList()
        //    };
        //    return View(viewModel);
        //}
        public IActionResult ShowReport(QuizViewModel model)
        {
            //var userAnswer = JsonConvert.DeserializeObject<Dictionary<int, int>>(TempData["UserAnswers"]?.ToString() ?? "{}");
            //var score = TempData["Score"] != null ? (int)TempData["Score"] : 0;
           var u =JsonConvert.DeserializeObject<Dictionary<int, int>>(HttpContext.Session.GetString("UserAnswers").ToString() ?? "{}");
            //            var quizData = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(
            //    HttpContext.Session.GetString("UserAnswers")
            //);
            var viewModel = new QuizViewModel
            {
                UserAnswers = u,
                Score = model.Score,
                Questions = _context.Questions.Include(p => p.Answers)
                .Select(q => new Question
                {
                    QuestionId = q.QuestionId,
                    Text = q.Text,
                    Answers = q.Answers
                }).ToList()

            };
            //var questions = _quizService.GetQuestionsWithAnswers();
            //model.Questions = questions;
            return View(viewModel);

            // Fetch all questions and correct answers from the database
            //var correctAnswers = _context.Answers
            //                             .Select(q => new
            //                             {
            //                                 q.Text,
            //                                 q.IsCorrect
            //                             }).ToList();

            //return View(correctAnswers);
        }
        
        public IActionResult Profile(User u)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return RedirectToAction("Login");
            }
            var userId = int.Parse(userIdClaim.Value);
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);
            return View(user);
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
