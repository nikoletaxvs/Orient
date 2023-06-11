using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orient.Models
{
    [Table("Question")]
    public partial class Question
    {
        public Question() {
            this.Answers = new HashSet<Answer>();
        }   
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public string Content { get; set; }

        public virtual ICollection<Answer> Answers { get;set; }
    }
}
