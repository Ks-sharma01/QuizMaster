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
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult AssignRole(int userId, string role)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                user.Role = role;
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
    }
}
