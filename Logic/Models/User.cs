using System.ComponentModel.DataAnnotations;

namespace Logic.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FIO { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; } = DateTime.Now;
    }
}
