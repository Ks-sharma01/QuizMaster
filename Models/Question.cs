using System.ComponentModel.DataAnnotations;

namespace QuizDishtv.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        public string Text { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public int CorrectAnswerId { get; internal set; }
    }
}
