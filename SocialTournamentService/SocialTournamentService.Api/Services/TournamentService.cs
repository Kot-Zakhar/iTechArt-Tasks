using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialTournamentService.SocialTournamentServiceDbContext;
using SocialTournamentService.SocialTournamentServiceDbContext.Models;

namespace SocialTournamentService.Api.Services
{
    public class TournamentService
    {
        private readonly TournamentServiceDbContext _context;
        private readonly PointsService _pointsService;
        private readonly UserService _userService;

        public TournamentService(
            TournamentServiceDbContext context, 
            PointsService pointsService, 
            UserService userService)
        {
            this._context = context;
            _pointsService = pointsService;
            _userService = userService;
        }


        public IQueryable<Tournament> GetTournaments()
        {
            return _context.Tournaments;
        }

        public async Task<Tournament> GetByIdAsync(Guid id)
        {
            return await _context.Tournaments.SingleAsync(t => t.Id == id);
        }

        public async Task<Tournament> UpdateTournamentAsync(Tournament tournamentData)
        {
            Tournament oldTournament = await GetByIdAsync(tournamentData.Id);
            if (oldTournament == null)
                return null;

            oldTournament.Name = tournamentData.Name ?? oldTournament.Name;
            oldTournament.Deposit = tournamentData.Deposit == 0 ? oldTournament.Deposit : tournamentData.Deposit;

            _context.Tournaments.Update(oldTournament);

            await _context.SaveChangesAsync();

            return oldTournament;
        }

        public async Task<Tournament> CreateAsync(Tournament tournamentData)
        {
            await _context.Tournaments.AddAsync(tournamentData);
            await _context.SaveChangesAsync();
            return await _context.Tournaments.SingleAsync(newTournament => newTournament.Id == tournamentData.Id);
        }
        public async Task<bool> TournamentExistsAsync(Guid id)
        {
            return (await _context.Tournaments.SingleAsync(t => t.Id == id)) != null;
        }

        public async Task Delete(Guid id)
        {
            Tournament tournament = _context.Tournaments.Find(id);
            if (tournament == null)
                return;

            _pointsService.CalculateTournamentPointsResult(tournament);

            await _userService.UpdateUserAsync(tournament.Winner);
            foreach (User user in tournament.Users)
                await _userService.UpdateUserAsync(user);

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> JoinUserAsync(Guid tournamentId, Guid userId)
        {
            User user = await _userService.GetByIdAsync(userId);
            Tournament tournament = await GetByIdAsync(tournamentId);

            if (user == null || tournament == null)
                return false;

            if (user.Balance < tournament.Deposit)
                return false;

            user.Balance -= tournament.Deposit;
            tournament.Prize += tournament.Deposit;

            await _userService.UpdateUserAsync(user);
            await UpdateTournamentAsync(tournament);
            
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task FinishTournamentAsync(Guid tournamentId)
        {
            Tournament tournament = await GetByIdAsync(tournamentId);

            if (tournament.Winner == null)
            {
                tournament.Winner = GetWinner(tournament);
            }

            _pointsService.CalculateTournamentPointsResult(tournament);

            await _userService.UpdateUserAsync(tournament.Winner);
            foreach (User user in tournament.Users)
                await _userService.UpdateUserAsync(user);

            await _context.SaveChangesAsync();
        }

        private User GetWinner(Tournament tournament)
        {
            var rand = new Random();
            int winnerIndex = rand.Next(0, tournament.Users.Count() - 1);
            return tournament.Users[winnerIndex];
        }
    }
}
