using Microsoft.EntityFrameworkCore;
using System;

namespace ShareMe.DataAccessLayer.Context
{
    public class ShareMeContext : DbContext
    {
        public ShareMeContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.useSqlServer("Server=(localdb)\mssqllocaldb;Database=ShareMeDB;Trusted_Connection=True;");
        }
    }
}
