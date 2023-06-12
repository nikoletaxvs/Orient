using Orient.Models;

namespace Orient.Interfaces
{
    public interface IAccountService
    {
        public Account Login(string username, string password);
        public string getFullName(string username);
        public string getEducation(string username);


        public int getTotalPoints(string username);


        public int getUnit1Times(string username);

    }
}
