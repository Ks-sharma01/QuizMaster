using System.ComponentModel.DataAnnotations;

namespace QuizDishtv.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }

       
    }
}
