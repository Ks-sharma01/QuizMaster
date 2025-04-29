namespace QuizDishtv.Models
{
    public class QuestionInputViewModel
    {
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public List<AnswerInputViewModel> Answers { get; set; }
    }
    public class AnswerInputViewModel
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
