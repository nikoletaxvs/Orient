using Orient.Models;

namespace Orient.Interfaces
{
    public interface IAccountStatistics
    {
        public void Update(AccountStatistics statistics);
        public AccountStatistics Get(int id);
        public IEnumerable<AccountStatistics> GetAll();
    }
}
