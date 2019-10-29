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
            optionsBUilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=moneymanagerdb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var generator = new RandomGenerator.MoneyManagerFaker();

            generator.Generate();

            modelBuilder.Entity<Category>().ToTable("Category").HasData(generator.categories.Select(c => RandomGenerator.CategoryFaker.ToPlainObject(c)));
            modelBuilder.Entity<User>().ToTable("User").HasData(generator.users.Select(u => RandomGenerator.UserFaker.ToPlainObject(u)));
            modelBuilder.Entity<Asset>().ToTable("Asset").HasData(generator.assets.Select(a => RandomGenerator.AssetFaker.ToPlainObject(a)));
            modelBuilder.Entity<Transaction>().ToTable("Transaction").HasData(generator.transactions.Select(t => RandomGenerator.TransactionFaker.ToPlainObject(t)));
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
