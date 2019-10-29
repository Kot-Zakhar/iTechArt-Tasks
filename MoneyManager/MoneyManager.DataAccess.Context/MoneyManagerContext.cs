using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccess.Entity;

namespace MoneyManager.DataAccess.Context
{
    public class MoneyManagerContext : DbContext
    {
        public MoneyManagerContext()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoneyManagerContext, Migrations.Configuration>());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
