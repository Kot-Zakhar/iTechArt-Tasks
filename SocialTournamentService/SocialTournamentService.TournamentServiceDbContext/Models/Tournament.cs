﻿using System;
using System.Collections.Generic;

namespace SocialTournamentService.SocialTournamentServiceDbContext.Models
{
    public class Tournament
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Deposit { get; set; }
        public int Prize { get; set; }
        public virtual IList<User> Users { get; set; } = new List<User>();
        public virtual User Winner { get; set; } = null;
    }
}
