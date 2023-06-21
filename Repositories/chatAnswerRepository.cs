using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Orient.Data;
using Orient.Interfaces;
using Orient.Models;

namespace Orient.Repositories
{
    public class chatAnswerRepository : IChatAnswer
    {
        private readonly ApplicationDbContect _dbContext;
        public chatAnswerRepository(ApplicationDbContect dbContect) {
            _dbContext = dbContect;
        }


        public IEnumerable<chatAnswer> GetChatAnswers()
        {
            return _dbContext.ChatAnswers.ToList();
        }
       
        public void AddAnswer(chatAnswer answer)
        {

            _dbContext.Add(answer);
            _dbContext.SaveChanges();
        }
    }
}
