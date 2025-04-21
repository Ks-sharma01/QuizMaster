using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizDishtv.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }

        public int Score { get; set; }

        public int UserId { get; set; }


        //public Category Category { get; set; }
        public User Users { get; set; }
    }
}
