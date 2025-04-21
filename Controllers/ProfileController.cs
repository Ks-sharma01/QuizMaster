using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizDishtv.Data;

namespace QuizDishtv.Controllers
{
    public class ProfileController : Controller
    {
        private readonly QuizDbContext _context;

        public ProfileController(QuizDbContext context)
        {
            _context = context;
        }
        public IActionResult History()
        {
            //var userId = HttpContext.Session.GetString("UserId");

            //var history = _context.UserAnswers
            //    .Include(ua => ua.Questions)
            //    .ThenInclude(q => q.C)
            return View();
        }
    }
}
