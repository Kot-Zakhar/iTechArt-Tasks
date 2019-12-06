using System;

namespace SocialTournamentService.SocialTournamentServiceDbContext.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public String Name { get; set; }
        public int Balance { get; set; }
    }
}
