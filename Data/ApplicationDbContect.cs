using Microsoft.EntityFrameworkCore;
using Orient.Models;

namespace Orient.Data
{
    public class ApplicationDbContect : DbContext
    {
        public ApplicationDbContect(DbContextOptions<ApplicationDbContect> options) : base(options) { }

        public DbSet<unit1_question> unit1_Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }

       public DbSet<Account> Accounts { get; set; }
       public DbSet<AccountStatistics> AccountStatistics { get; set; }

       public DbSet<DaySector> DaySectors { get; set; }
        public DbSet<Part> Parts { get; set; } 
    }
}
