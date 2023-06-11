namespace Orient.Models
{
    public class Repliy
    {
        public int ReplyId { get; set; }
        public int TotalScore { get; set; }
        List<Question> QuestionList { get; set; }
        List<Answer> AnswersList { get; set;}
    }
}
