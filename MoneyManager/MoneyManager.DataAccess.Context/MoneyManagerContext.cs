using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccess.Entity;
using System.Linq;

namespace MoneyManager.DataAccess.Context
{
    public class MoneyManagerContext : DbContext
    {
        public MoneyManagerContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBUilder)
        {
            optionsBUilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MoneyManagerLocalDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Asset>().ToTable("Asset");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
