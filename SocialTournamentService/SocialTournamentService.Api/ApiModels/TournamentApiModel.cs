using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialTournamentService.SocialTournamentServiceDbContext.Models;

namespace SocialTournamentService.Api.ApiModels
{
    public class TournamentApiModel
    {
        public TournamentApiModel() { }
        public TournamentApiModel(Tournament tournament)
        {
            Id = tournament.Id;
            Name = tournament.Name;
            Deposit = tournament.Deposit;
            Prize = tournament.Prize;
            Users = tournament.Users.Select(u => u.Id).ToList();
            Winner = tournament.Winner?.Id ?? Guid.Empty;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Deposit { get; set; }
        public int Prize { get; set; }
        public IList<Guid> Users { get; set; } = new List<Guid>();
        public Guid Winner { get; set; } = Guid.Empty;
    }
}
