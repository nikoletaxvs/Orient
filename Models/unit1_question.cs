using System.ComponentModel.DataAnnotations;

namespace Orient.Models
{
    public class unit1_question
    {
        [Key]
        public int id { get; set; }
        [Required] 
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string correctAnswear { get; set; }
        [Required]
        public string answear1 { get; set; }
        [Required]
        public string answear2 { get; set; }
        [Required]
        public string answear3 { get; set; }
        [Required]
        public string answear4 { get; set; }

    }
}
