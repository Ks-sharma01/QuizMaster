using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizDishtv.Models
{
    public class UserAnswer
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Questions { get; set; }

        [ForeignKey("Answer")]
        public int SelectedAnswerId { get; set; }
        public Answer SelectedAnswer { get; set; }
    }
}
