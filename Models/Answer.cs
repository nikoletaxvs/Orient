using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orient.Models
{
    [Table("Answer")]
    public partial class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public bool Correct { get; set; }
        [Required]
        //Foreign key for Standard
        public int QuestionId { get; set; }
        public virtual Question Questions { get; set; }
    }
}
