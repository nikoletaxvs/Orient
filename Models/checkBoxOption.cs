using System.ComponentModel.DataAnnotations;

namespace Orient.Models
{
    public class checkBoxOption
    {
        [Key]
        public int id { get; set; }
        [Required]
        public bool IsChecked { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
