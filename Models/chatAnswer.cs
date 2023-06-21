using System.ComponentModel.DataAnnotations;

namespace Orient.Models
{
    public class chatAnswer
    {
        [Key]
        public int Id { get; set; }
        
        public string? Message { get; set; }
        [Required]
        public string? Name { get; set; }

        public string? Date { get; set; }
    }
}
