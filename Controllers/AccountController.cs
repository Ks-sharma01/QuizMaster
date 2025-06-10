using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizDishtv.Data;
using QuizDishtv.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using QuizMaster.Services;

namespace QuizDishtv.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuizDbContext _context;
        private readonly IUserService _userService;

        public AccountController(QuizDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
     
        public IActionResult Register()
        {
            return View();
        } 
        
        [HttpPost]
        public async Task<IActionResult> Register(User u)
        {
            if (ModelState.IsValid)
            {
                //var user = await _userService.Register();
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserEmail == u.UserEmail);
                if (user != null)
                {
                    ModelState.AddModelError("UserEmail", "Email is already registered");
                    return View(u);
                }
               u.UserPassword= _userService.HashPassword(u.UserPassword);
                //    u.UserPassword = HashPassword(u.UserPassword);
                    u.Role = "User";
                    _context.Users.Add(u);
                    await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Account");
            }
            return View(u);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string UserEmail, string UserPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserEmail == UserEmail);
                if (user != null && _userService.VerifyPassword(UserPassword, user.UserPassword))
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.UserEmail),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("Userid", user.UserId.ToString())

                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(
                       CookieAuthenticationDefaults.AuthenticationScheme,
                       new ClaimsPrincipal(claimsIdentity), authProperties);
                   if(user.Role == "Admin")
                    {
                        return RedirectToAction("SelectCategory", "Admin");

                    }
                    return RedirectToAction("Index", "Home");
                }


            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
