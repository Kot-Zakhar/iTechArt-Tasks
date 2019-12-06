using System;
using System.Collections.Generic;
using System.Text;

namespace SocialTournamentService.TournamentServiceDbContext.Models
{
    public class Tournament
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public String Name { get; set; }
        public int Deposit { get; set; }
        public int Prize { get; set; }
        public virtual IList<User> Users { get; set; }
        public virtual User Winner { get; set; }
    }
}
