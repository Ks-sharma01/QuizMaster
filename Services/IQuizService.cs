using QuizDishtv.Models;

public interface IQuizService
{
    Task AddQuestionAsync(QuestionInputViewModel model);
}
