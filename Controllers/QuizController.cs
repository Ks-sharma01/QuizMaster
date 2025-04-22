using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizDishtv.Data;
using QuizDishtv.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

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
        public IActionResult Categories()
        {
            var categories = _context.Category.ToList();
            return View(categories);
        }
        //*************
        //[Authorize]
        //public IActionResult Index(QuizViewModel viewModel, int id)
        //{
        //    var cateid = _context.Questions.FirstOrDefault(temp => temp.CategoryId == id);
        //    if (cateid != null)
        //    {
        //       viewModel = new QuizViewModel
        //       {
        //       Questions = _context.Questions.Where(r => r.CategoryId == id)
        //       .Include(q => q.Answers)
        //       .Select(p => new Question
        //       {
        //           QuestionId = p.QuestionId,
        //           Text = p.Text,
        //           Answers = p.Answers,
        //       }).ToList()
        //       };
        //    } 

        //    TempData["Count"] = viewModel.Questions.Count;
        //    TempData["id"] = id;
        //    //TempData["Category"] = _context.Category.Select(temp => temp.Name);
        //    return View("Index", viewModel);

        //    //if(id == cateid)
        //    //{
        //    //    viewModel = new QuizViewModel
        //    //    {
        //    //        Questions = _context.Questions.Where(r => r.CategoryId == id)
        //    //  .Include(q => q.Answers)
        //    //  .Select(p => new Question
        //    //  {
        //    //      QuestionId = p.QuestionId,
        //    //      Text = p.Text,
        //    //      Answers = p.Answers
        //    //  }).ToList()

        //    //    };

        //    //}
        //    //if(id == cateid)
        //    //{
        //    //    viewModel = new QuizViewModel
        //    //    {
        //    //        Questions = _context.Questions.Where(r => r.CategoryId == id)
        //    //  .Include(q => q.Answers)
        //    //  .Select(p => new Question
        //    //  {
        //    //      QuestionId = p.QuestionId,
        //    //      Text = p.Text,
        //    //      Answers = p.Answers
        //    //  }).ToList()

        //    //    };

        //    //}
        //    //var viewModel = new QuizViewModel
        //    //{
        //    //    Questions = _context.Questions.Where(r => r.CategoryId == id)
        //    //.Include(q => q.Answers)
        //    //.Select(p => new Question
        //    //{
        //    //    QuestionId = p.QuestionId,
        //    //    Text = p.Text,
        //    //    Answers = p.Answers
        //    //}).ToList()

        //    //};
        //    //var questions = _context.Questions.Select(q => new
        //    //{
        //    //    Text = q.Text,
        //    //    CategoryId = q.CategoryId
        //    //}).ToList();

        //    //if (string.IsNullOrEmpty(tableName))
        //    //{
        //    //    return BadRequest("Table name cannot be null or empty.");
        //    //}
        //    //object viewModel = null;

        //    //switch (tableName) 
        //    //{
        //        //case "Maths":
        //        //    viewModel = _context.Maths
        //        //        .Include(q => q.MathOptions) // Assuming Maths has a navigation property MathOptions
        //        //        .Select(p => new 
        //        //        {
        //        //            Id = p.Id,
        //        //            Text = p.Text,
        //        //            MathOptions = p.MathOptions
        //        //        }).ToList();
        //        //   TempData["Count"] = viewModel;
        //        //    break;

        //        //case "Category":
        //        //    viewModel = _context.Category
        //        //        .Select(p => new Category
        //        //        {
        //        //            CategoryId = p.CategoryId,
        //        //            Name = p.Name
        //        //        }).ToList();
        //        //    TempData["Count"] = ((List<Category>)viewModel).Count;
        //        //    break;

        //    //    default:
        //    //        return NotFound($"Table '{tableName}' is not recognized.");
        //    //}

        //    //return View(viewModel);
        //}
        // switch (tableName)
        //{
        // case "Maths":
        // {
        //             //var viewModel = new QuizViewModel
        //             // {
        //             tableName = _context.tableName
        //             .Include(q => q.MathOptions)
        //             .Select(p => new Maths
        //             {
        //                 Id = p.Id,
        //                 Text = p.Text,
        //                 MathOptions = p.MathOptions
        //             }).ToList();
        //             //};

        //            //.ToList();
        //             //};
        //             TempData["Count"] = viewModel.Maths.Count;
        //             return View(viewModel);

        //    }
        //}
        // return View();
        //return View(data);

        //var viewModel = new QuizViewModel
        //{
        //    Questions = _context.Questions
        //.Include(q => q.Answers)
        //.Select(p => new Question
        //{
        //    QuestionId = p.QuestionId,
        //    Text = p.Text,
        //    Answers = p.Answers
        //}).ToList()

        //};

        //TempData["Count"] = viewModel.Questions.Count;
        //return View(viewModel);

        //***********

        //[Authorize]
        //[HttpPost]
        //public IActionResult SubmitAnswer(QuizViewModel model, int id)
        //{
        //model.Score = 0;

        //foreach (var answer in model.UserAnswers)
        //{
        //    var correctAnswer = _context.Answers.FirstOrDefault(a => a.AnswerId == answer.Value && a.IsCorrect);
        //    if (correctAnswer != null)
        //    {
        //        model.Score++;   
        //    }
        //}
        //TempData["Score"] = model.Score;
        ////HttpContext.Session.SetString("UserAnswers", System.Text.Json.JsonSerializer.Serialize(model.UserAnswers));
        //HttpContext.Session.SetString("UserAnswers", JsonConvert.SerializeObject(model.UserAnswers));
        ////HttpContext.Session.SetInt32("Score", model.Score);
        //var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.UserId;
        ////var QuizCategory = _context.Category.FirstOrDefault(temp => temp.CategoryId == id);
        //var QuizResult = _context.Results.FirstOrDefault(temp => temp.UserId == userId);

        //if (QuizResult != null)
        //{
        //    QuizResult.Score= model.Score;

        //}
        //else
        //{
        //     QuizResult = new Result
        //    {
        //        Score = model.Score,
        //        UserId = userId.Value,
        //        //CategoryId = (int)TempData["id"],

        //    };
        //    _context.Results.Add(QuizResult);
        //}
        //_context.SaveChanges();
        ////if(userId != null)
        ////{
        ////    var result = new Result
        ////    {
        ////        Score = model.Score,
        ////        UserId = userId.Value,

        ////    };
        ////    _context.Results.Add(result);
        ////    _context.SaveChanges();

        ////var viewModel = new QuizViewModel
        ////{
        ////    UserAnswers = model.UserAnswers,
        ////};
        ////}   
        //return View("Result", model);

        //}

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
            //var userId = int.Parse(User.FindFirst("UserId")?.Value);
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

        public IActionResult ShowResult (int categoryId)
        {
            //var userId = int.Parse(User.FindFirst("UserId")?.Value);
            //var answers = _context.UserAnswer
            //    .Include(ua => ua.SelectedAnswer.Text)
            //    .Include(ua => ua.Question.QuestionId)
            //    .Include(ua => ua.Category.Name)
            //    .Where(ua => ua.UserId == userId && ua.Question.CategoryId == categoryId).ToList();
            //var result = _context.Results.Where(x => x.CategoryId == categoryId && x.UserId == userId).FirstOrDefault();
            //int score = answers.Count(a => a.SelectedAnswer.IsCorrect);
            //if(score != null)
            //{
            //     result = new Result { 
            //         Score = score,
            //         CategoryId = categoryId,
            //         UserId = userId,
            //         AttemptedOn = DateTime.Now,
            //     };
            //}
            //_context.Results.Add(result);
            //_context.SaveChanges();
            //ViewBag.Score = score;
            //ViewBag.Total = answers.Count;

            //return View(answers);

        
            //int userId = int.Parse(User.FindFirstValue("UserId"));
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserId;


            var answers = _context.UserAnswer
                .Include(ua => ua.SelectedAnswer)
                .Include(ua => ua.Question)
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
            ViewBag.Total = answers.Count;

            return View(answers);
        }

        

        //*************

        //public IActionResult ShowReport(QuizViewModel model)
        //{
        //    //var userAnswer = JsonConvert.DeserializeObject<Dictionary<int, int>>(TempData["UserAnswers"]?.ToString() ?? "{}");
        //    //var score = TempData["Score"] != null ? (int)TempData["Score"] : 0;
        //   var u = JsonConvert.DeserializeObject<Dictionary<int, int>>(HttpContext.Session.GetString("UserAnswers").ToString() ?? "{}");
        //    //            var quizData = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(
        //    //    HttpContext.Session.GetString("UserAnswers")
        //    //);
        //    var viewModel = new QuizViewModel
        //    {
        //        UserAnswers = u,
        //        Score = model.Score,
        //        Questions = _context.Questions.Include(p => p.Answers)
        //        .Select(q => new Question
        //        {
        //            QuestionId = q.QuestionId,
        //            Text = q.Text,
        //            Answers = q.Answers
        //        }).ToList()

        //    };
        //    //var questions = _quizService.GetQuestionsWithAnswers();
        //    //model.Questions = questions;
        //    return View(viewModel);
        //}

        // Fetch all questions and correct answers from the database
        //var correctAnswers = _context.Answers
        //                             .Select(q => new
        //                             {
        //                                 q.Text,
        //                                 q.IsCorrect
        //                             }).ToList();

        //return View(correctAnswers);
        [Authorize]
        public IActionResult SelectQuiz()
        {
            var subject = _context.Category.Select(p => new {p.CategoryId, p.Name});
            return View(subject);
        }
        
        public IActionResult Profile()
        {
            //var userId = int.Parse(User.FindFirstValue("UserId"));
            var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserId;

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            var quizAttempts = _context.Results.Include(a => a.Category)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.AttemptedOn).ToList();
            if(quizAttempts == null)
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
            //int total = (int)TempData["Total"];
           
            return View(viewModel);

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
