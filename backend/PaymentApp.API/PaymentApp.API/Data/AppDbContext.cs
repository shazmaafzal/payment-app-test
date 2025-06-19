using Microsoft.EntityFrameworkCore;
using PaymentApp.API.Models;

namespace PaymentApp.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<RefundRequest> RefundRequests { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasKey(t => t.Id);
            modelBuilder.Entity<RefundRequest>().HasKey(c => c.TransactionId);
            modelBuilder.Entity<Transactions>().HasKey(c => c.Id);
            modelBuilder.Entity<Card>().HasData(
                new Card
                {
                    Id = 1,
                    CardNumber = "1234567812345678",
                    CardHolderName = "John Doe",
                    ExpiryDate = new DateTime(2026, 12, 31),
                    IsActive = true,
                    Balance = 1000,
                    CVV = 123
                },
                new Card
                {
                    Id = 2,
                    CardNumber = "8765432187654321",
                    CardHolderName = "Jane Smith",
                    ExpiryDate = new DateTime(2025, 11, 30),
                    IsActive = true,
                    Balance = 1500,
                    CVV = 456
                },
                new Card
                {
                    Id = 3,
                    CardNumber = "4444333322221111",
                    CardHolderName = "Alice Johnson",
                    ExpiryDate = new DateTime(2027, 06, 15),
                    IsActive = true,
                    Balance = 2000,
                    CVV = 789
                },
                new Card
                {
                    Id = 4,
                    CardNumber = "9999888877776666",
                    CardHolderName = "Bob Williams",
                    ExpiryDate = new DateTime(2024, 12, 31),
                    IsActive = false,
                    Balance = 500,
                    CVV = 765
                },
                new Card
                {
                    Id = 5,
                    CardNumber = "1111222233334444",
                    CardHolderName = "Charlie Brown",
                    ExpiryDate = new DateTime(2025, 08, 01),
                    IsActive = true,
                    Balance = 750,
                    CVV = 543
                }
            );

        }
    }
}
