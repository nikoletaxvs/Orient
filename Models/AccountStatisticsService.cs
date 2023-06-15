using Orient.Interfaces;

namespace Orient.Models
{
    public class AccountStatisticsService:IAccountStatistics
    {
        public List<AccountStatistics> statistics;

        public AccountStatisticsService()
        {
            
               
            
            
        }

        public AccountStatistics Get(int id)
        {
            return statistics[id];
        }
        public IEnumerable<AccountStatistics> GetAll()
        {
            return statistics.ToList();
        }
        

        public void Update(AccountStatistics statistics)
        {
            throw new NotImplementedException();
        }
    }
}
