using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialTournamentService.SocialTournamentServiceDbContext;
using SocialTournamentService.SocialTournamentServiceDbContext.Models;

namespace SocialTournamentService.Api.Services
{
    public class PointsService
    {
        public bool TakePoints(User user, int points)
        {
            if (user.Balance >= points)
            {
                user.Balance -= points;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddPoints(User user, int points)
        {
            user.Balance += points;
        }

        public void CalculateTournamentPointsResult(Tournament tournament)
        {
            if (tournament.Winner == null)
                foreach (var tournamentUser in tournament.Users)
                    tournamentUser.Balance += tournament.Deposit;
            else if (tournament.Prize != 0)
            {
                tournament.Winner.Balance += tournament.Prize;
                tournament.Prize = 0;
            }
        }
    }
}
