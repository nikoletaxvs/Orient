using Orient.Models;

namespace Orient.Interfaces
{
    public interface IAccountStatistics
    {
        public void Update(AccountStatistics statistics);
        public IEnumerable<AccountStatistics> GetAll();
        public AccountStatistics GetByAccountId(int id);
    }
}
