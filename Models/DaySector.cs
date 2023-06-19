using System.ComponentModel.DataAnnotations;

namespace Orient.Models
{
    public class DaySector
    {
        [Key]
        public int DaySectorId { get; set; }
        [Required]
        public string Sector { get; set; }

        public ICollection<Part> Parts { get; set; }
    }
}
