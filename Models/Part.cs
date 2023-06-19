using System.ComponentModel.DataAnnotations;

namespace Orient.Models
{
    public class Part
    {
        [Key]
        public int PartId { get; set; }
        [Required]
        public string PartText { get; set; }
        [Required]
        public string PartCareer { get; set; }
        [Required]
        public string PartIcon { get; set; }

        //Foreign key for Standard
        public int DaySectorId { get; set; }
        public DaySector DaySector { get; set; }
    }
}
