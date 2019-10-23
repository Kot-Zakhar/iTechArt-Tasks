using System.Data.Entity;
using MoneyManager.Entity;

namespace MoneyManager.DB
{
    public class MoneyManagerContext : DbContext
    {
        public MoneyManagerContext(): base("MoneyManagerDB")
        {
            Database.SetInitializer(new MoneyManagerDBInitializer());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
