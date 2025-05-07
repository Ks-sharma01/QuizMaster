using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizDishtv.Models;
using QuizDishtv.ViewModels;

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
        public DbSet<UserAnswer> UserAnswer { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionAnswerDto>().HasNoKey();
            modelBuilder.Entity<Question>()
    .HasMany(q => q.Answers)
    .WithOne(a => a.Questions)
    .OnDelete(DeleteBehavior.Cascade);

        }
    }
    }
