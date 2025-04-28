using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizDishtv.Data;
using QuizDishtv.Models;
using System.Data;

namespace QuizDishtv.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly QuizDbContext _context;

        public AdminController(QuizDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            return View();
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
                _context.SaveChanges();
            }
            return View(user);
        }
    }
}
