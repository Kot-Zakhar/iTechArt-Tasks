using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace MoneyManager
{
    public class MoneyManagerContext : DbContext
    {
        public MoneyManagerContext()
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
