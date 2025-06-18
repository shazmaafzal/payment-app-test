using Microsoft.EntityFrameworkCore;
using PaymentApp.API.Models;

namespace PaymentApp.API.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<RefundRequest> RefundRequests { get; set; }
        public DbSet<Transactions> Transactionss { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasKey(t => t.Id);
            modelBuilder.Entity<RefundRequest>().HasKey(c => c.TransactionId);
            modelBuilder.Entity<Transactions>().HasKey(c => c.Id);
        }
    }
}
