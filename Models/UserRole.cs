using System.ComponentModel.DataAnnotations;

namespace QuizDishtv.Models
{
    public class UserRole
    {
        [Key]
        public int RoleId { get; set; }

        public string Role { get; set; }

        public int UserId { get; set; }
        public User users { get; set; }
    }
}
