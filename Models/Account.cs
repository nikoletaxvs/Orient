using System.ComponentModel.DataAnnotations;

namespace Orient.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string EducationLevel { get; set; }
      
    }
}
