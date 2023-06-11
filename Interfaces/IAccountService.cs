using Orient.Models;

namespace Orient.Interfaces
{
    public interface IAccountService
    {
        public Account Login(string username, string password);
    }
}
