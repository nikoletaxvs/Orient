using Orient.Interfaces;
using System.Linq;

namespace Orient.Models
{
    public class AccountService : IAccountService
    {
        public List<Account> Accounts;
        
        public AccountService() { 
            Accounts=new List<Account>() {
                new Account() {
                    Id= 1,
                    UserName="user1",
                    Password="123",
                    FullName="Adelina Kazantzidi",
                    EducationLevel="Undergraduate",
                    TotalPoints=1,
                    Unit1Times=0
                },
                new Account()
                {
                    Id= 2,
                    UserName="user2",
                    Password="123",
                    FullName="Nikol Koliatsou",
                    EducationLevel="Undergraduate",
                    TotalPoints=1,
                    Unit1Times=0
                }
            
            };
        }
        public Account Login(string username, string password)
        {
            return Accounts.SingleOrDefault(a => a.UserName == username && a.Password == password);
        }
        public string getFullName(string username) {
            return Accounts.SingleOrDefault(a => a.UserName == username).FullName;
        }
        public string getEducation(string username) {
            return Accounts.SingleOrDefault(a => a.UserName == username).EducationLevel;
        }public int getTotalPoints(string username) {
            return Accounts.SingleOrDefault(a => a.UserName == username).TotalPoints;
        }public int getUnit1Times(string username) {
            return Accounts.SingleOrDefault(a => a.UserName == username).Unit1Times;
        }
    }
}
