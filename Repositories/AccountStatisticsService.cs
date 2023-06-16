using Orient.Data;
using Orient.Interfaces;
using Orient.Models;

namespace Orient.Repositories
{
    public class AccountStatisticsService : IAccountStatistics
    {
        private readonly ApplicationDbContect _dbContext;
        public List<AccountStatistics> statistics;

        public AccountStatisticsService(ApplicationDbContect dbContect)
        {
            _dbContext= dbContect;

        }
        
        public AccountStatistics GetByAccountId(int id)
        {
            return _dbContext.AccountStatistics.Where(a=> a.AccountId == id).FirstOrDefault();
        }
        public IEnumerable<AccountStatistics> GetAll()
        {
            return _dbContext.AccountStatistics.ToList();
        }


        public void Update(AccountStatistics statistics)
        {
            _dbContext.Update(statistics);
            _dbContext.SaveChanges();
            
        }
    }
}
