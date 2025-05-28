using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizDishtv.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int Score { get; set; }
        public DateTime AttemptedOn { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
    }
}
