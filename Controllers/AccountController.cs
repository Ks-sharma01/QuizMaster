using Microsoft.AspNetCore.Mvc;
using QuizDishtv.Data;
using QuizDishtv.Models;

namespace QuizDishtv.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuizDbContext _context;
        public AccountController(QuizDbContext context)
        {
            _context = context;
        }
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var AddingUser = _context.Users.Add(user);
                _context.SaveChanges();
                return View(AddingUser);
            }
            return View(user);
        }
    }
}
