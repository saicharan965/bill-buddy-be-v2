using BillBuddy.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BillBuddy.API.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options):base(options) {}
        public DbSet<SplitTransactionParticipant> SplitTransactionParticipants { get; set; }
        public DbSet<SplitTransaction> SplitTransactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
