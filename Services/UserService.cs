using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizDishtv.Data;
using QuizDishtv.Models;
using System.Security.Claims;
using System.Security.Cryptography;

namespace QuizMaster.Services
{
    public class UserService : IUserService
    {
        private readonly QuizDbContext _context;

        public UserService(QuizDbContext context)
        {
            _context = context;
        }
        public string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        //private bool VerifyPassword(string enteredPassword, string storedPassword)
        //{
        //    var parts = storedPassword.Split('.');
        //    if (parts.Length != 2)
        //    {
        //        return false;
        //    }

        //    var salt = Convert.FromBase64String(parts[0]);
        //    var storedHash = parts[1];

        //    string enteredHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //        password: enteredPassword,
        //        salt: salt,
        //        prf: KeyDerivationPrf.HMACSHA256,
        //        iterationCount: 10000,
        //        numBytesRequested: 256 / 8));

        //    return storedHash == enteredHash;
        //}

        //public async Task<User> Register(User u)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserEmail == u.UserEmail);
        //        if (user != null)
        //        {
        //            //ModelState.AddModelError("UserEmail", "Email is already registered");
        //            return View(u);
        //        }

        //        u.UserPassword = HashPassword(u.UserPassword);
        //        u.Role = "User";
        //        _context.Users.Add(u);
        //        await _context.SaveChangesAsync();

        //    return RedirectToAction("Login", "Account");
        //}
        //    return View(u);
        //}
       
            public bool VerifyPassword(string enteredPassword, string storedPassword)
            {
            var parts = storedPassword.Split('.');
            if (parts.Length != 2)
            {
                return false;
            }

            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            string enteredHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return storedHash == enteredHash;
        }
        

      

    }
}
