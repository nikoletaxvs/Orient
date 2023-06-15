using Orient.Models;

namespace Orient.Interfaces
{
    public interface IAccountService
    {
        public Account Login(string username, string password);
        public string getFullName(string username);
        public string getEducation(string username);


      

    }
}
