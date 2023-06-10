using Microsoft.EntityFrameworkCore;
using Orient.Models;

namespace Orient.Data
{
    public class ApplicationDbContect :DbContext
    {
        public ApplicationDbContect(DbContextOptions<ApplicationDbContect> options) : base(options) { } 
       
        public DbSet<unit1_question> unit1_Questions { get; set; }
        public DbSet<checkBoxOption> checkbox_answears { get; set; }
    }
}
