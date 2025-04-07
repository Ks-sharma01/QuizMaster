using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizDishtv.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        public string Text { get; set; }
        //public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; }
        //public bool IsCorrect => UserAnswer == IsCorrect,


        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
