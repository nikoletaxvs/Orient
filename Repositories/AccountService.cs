using Microsoft.EntityFrameworkCore;
using Orient.Data;
using Orient.Interfaces;
using Orient.Models;
using System;
using System.Linq;

namespace Orient.Repositories
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContect _dbContext;
        public List<Account> Accounts;

        public AccountService(ApplicationDbContect dbContext)
        {
            _dbContext = dbContext;
            Accounts = GetAccounts();

        }
        public List<Account> GetAccounts()
        {
            return _dbContext.Accounts.ToList();
        }
        public Account Login(string username, string password)
        {
            return Accounts.SingleOrDefault(a => a.UserName == username && a.Password == password);
        }
        public int getAccountId(string username)
        {
            return _dbContext.Accounts.SingleOrDefault(a => a.UserName == username).Id;
        }
        public string getFullName(string username)
        {
            return Accounts.SingleOrDefault(a => a.UserName == username).FullName;
        }
        public string getEducation(string username)
        {
            return Accounts.SingleOrDefault(a => a.UserName == username).EducationLevel;
        }
    }
}
