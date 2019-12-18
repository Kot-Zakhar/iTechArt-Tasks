using Microsoft.EntityFrameworkCore;
using SocialTournamentService.SocialTournamentServiceDbContext.Models;

namespace SocialTournamentService.SocialTournamentServiceDbContext
{
    public class TournamentServiceDbContext : DbContext
    {
        public TournamentServiceDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
    }
}
