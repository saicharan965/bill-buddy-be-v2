using BillBuddy.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BillBuddy.API.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options):base(options) {}
        public DbSet<Group> Groups { get; set; }
        public DbSet<SplitTransactionParticipant> Participants { get; set; }
        public DbSet<SplitTransaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
