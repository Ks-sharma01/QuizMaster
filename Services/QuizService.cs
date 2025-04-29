using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuizDishtv.Data;
using QuizDishtv.Models;

public class QuizService : IQuizService
{
    private readonly QuizDbContext _context;

    public QuizService(QuizDbContext context)
    {
        _context = context;
    }

    public async Task AddQuestionAsync(QuestionInputViewModel model)
    {
        var parameters = new[]
        {
            new SqlParameter("@Text", model.Text),
            new SqlParameter("@CategoryId", model.CategoryId)
        };

        var questionId = await _context.Database
            .ExecuteSqlRawAsync("EXEC AddQuestion @Text, @CategoryId", parameters);

        var insertedQuestion = await _context.Questions
            .OrderByDescending(q => q.QuestionId)
            .FirstOrDefaultAsync(q => q.Text == model.Text);

        foreach (var answer in model.Answers)
        {
            if(insertedQuestion != null)
            {
                await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddAnswer @QuestionId = {0}, @Text = {1}, @IsCorrect = {2}",
                insertedQuestion.QuestionId, answer.Text, answer.IsCorrect
            );
            }
        }
    }
}
