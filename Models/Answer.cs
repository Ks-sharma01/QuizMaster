using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizDishtv.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        [ForeignKey("Question")]    
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
