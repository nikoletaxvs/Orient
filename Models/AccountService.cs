using Orient.Interfaces;

namespace Orient.Models
{
    public class AccountService : IAccountService
    {
        public List<Account> Accounts;
        
        public AccountService() { 
            Accounts=new List<Account>() {
                new Account() {
                    UserName="user1",
                    Password="123",
                    FullName="Andelina Kazana"
                
                },
                new Account()
                {
                    UserName="user2",
                    Password="123",
                    FullName="Nikol Koliatsou"
                }
            
            };
        }
        public Account Login(string username, string password)
        {
            return Accounts.SingleOrDefault(a => a.UserName == username && a.Password == password);
        }
    }
}
