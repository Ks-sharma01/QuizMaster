using Microsoft.EntityFrameworkCore;
using QuizDishtv.Models;

namespace QuizDishtv.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
