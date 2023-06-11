namespace Orient.Models
{
    public class Reply
    {
       // public int ReplyId { get; set; }
        public int TotalScore { get; set; }
        public List<Question> QuestionList { get; set; }
        public List<Answer> AnswersList { get; set;}
        public List<bool> correctAnswers { get; set; }
    }
}
