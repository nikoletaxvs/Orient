using System.ComponentModel.DataAnnotations;

namespace Orient.Models
{
    public class AccountStatistics
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }
        //software engineering
        [Required]
        public int softwareEngineeringAttempts { get; set; } = 0;
        [Required]
        public int softwareEngineeringCompletions { get; set; } = 0;
        [Required] public int softwareEnginneringMeanScore { get; set; } = 0; 
        //data science
        [Required] public int dataScienceingAttempts { get; set; } = 0;
        [Required] public int dataScienceCompletions { get; set; } = 0;
        [Required] public int dataSciencegMeanScore { get; set; } = 0;
        //ux deisgn
        [Required] public int UXAttempts { get; set; } = 0;
        [Required] public int UXCompletions { get; set; } = 0;  
        [Required] public int UXMeanScore { get; set; } = 0;
        // game development
         [Required] public int gameAttempts { get; set; } = 0;
        [Required] public int gameCompletions { get; set; } = 0;
        [Required] public int gameMeanScore { get; set; } = 0;
        //machine learing
        [Required] public int msAttempts { get; set; } = 0;
        [Required] public int msCompletions { get; set; } = 0;
        [Required] public int msMeanScore { get; set; } = 0;

        [Required] public int loginCount { get; set; } = 0;
    }
}
