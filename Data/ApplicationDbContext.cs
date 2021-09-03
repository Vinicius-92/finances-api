using System.Threading.Tasks;
using FinancesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancesAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Wallet>(s => s.MyWallet)
                .WithOne(u => u.User)
                .HasForeignKey<Wallet>(x => x.UserId);
           
            modelBuilder.Entity<CurrencyInvestiments>()
                .HasOne<Wallet>(x => x.Wallet)
                .WithMany(x => x.CurrencyInvestiments);
        }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyInvestiments> CurrencyInvestiments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}