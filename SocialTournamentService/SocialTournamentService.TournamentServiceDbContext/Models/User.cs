using System;
using System.Collections.Generic;
using System.Text;

namespace SocialTournamentService.TournamentServiceDbContext.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public String Name { get; set; }
        public int Balance { get; set; }
    }
}
