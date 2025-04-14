using System.Collections.ObjectModel;

namespace QuizDishtv.Models
{
    public class QuizViewModel
    {
       
        public List<Question> Questions { get; set; }
        public Dictionary<int, int> UserAnswers { get; set; } = new Dictionary<int, int>();
        public int Score { get; set; }
        public Result Results { get; set; }
        public Answer Answers { get; set; }
        public Category Category { get; set; }
        public User Users { get; set; }
      
    }
}
