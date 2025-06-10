using Microsoft.AspNetCore.Mvc;

namespace QuizMaster.Services
{
    public interface IUserService
    {
        string HashPassword(string password);
        bool VerifyPassword(string enteredPassword, string storedPassword);
    }
}
