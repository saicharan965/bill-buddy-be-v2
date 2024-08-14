using BillBuddy.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BillBuddy.API.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options):base(options) {}
        public DbSet<Group> Groups { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
