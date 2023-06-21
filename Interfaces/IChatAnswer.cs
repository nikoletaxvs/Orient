using Orient.Models;

namespace Orient.Interfaces
{
    public interface IChatAnswer
    {
        public IEnumerable<chatAnswer> GetChatAnswers();
        public void AddAnswer(chatAnswer answer);

    }
}
