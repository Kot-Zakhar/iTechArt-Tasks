using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialTournamentService.Api.ApiModels;
using SocialTournamentService.SocialTournamentServiceDbContext;
using SocialTournamentService.SocialTournamentServiceDbContext.Models;

namespace SocialTournamentService.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly TournamentServiceDbContext _context;

        public TournamentController(TournamentServiceDbContext context)
        {
            _context = context;
        }

        // GET: Tournament
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentApiModel>>> GetTournaments()
        {
            var s = await _context.Tournaments.ToListAsync();
            var result = s.Select(t => new TournamentApiModel(t)).ToList();
            return result;
        }

        // GET: Tournament/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentApiModel>> GetTournament(Guid id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            
            if (tournament == null)
            {
                return NotFound();
            }

            return new TournamentApiModel(tournament);
        }

        // PUT: Tournament/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTournament(Guid id, [FromBody] TournamentApiModel tournamentData)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            
            tournament.Name = tournamentData.Name ?? tournament.Name;
            tournament.Deposit = tournamentData.Deposit == 0 ? tournament.Deposit : tournamentData.Deposit;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: Tournament
        [HttpPost]
        public async Task<ActionResult<object>> PostTournament([FromBody] TournamentApiModel tournamentData)
        {
            var tournament = new Tournament()
            {
                Name = tournamentData.Name,
                Deposit = tournamentData.Deposit
            };

            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();

            return new { id = tournament.Id };
        }

        // DELETE: Tournament/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTournament(Guid id)
        {
            Tournament tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            if (tournament.Winner != null)
            {
                foreach (var tournamentUser in tournament.Users)
                {
                    tournamentUser.Balance += tournament.Deposit;
                }
            }

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: Tournament/4/join
        [HttpPost("{id}/join")]
        public async Task<ActionResult> JoinTournament(Guid id, [FromBody] Guid userId)
        {
            Tournament tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound("Such tournament doesn't exist.");
            }
            User user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("Such user doesn't exist.");

            // should i make a call to UserController here to withdraw deposit?
            if (user.Balance < tournament.Deposit)
                return BadRequest("User balance is lower, than deposit.");

            if (tournament.Users.Any(u => u.Id == user.Id))
                return BadRequest("User has been already registered as a participant.");

            user.Balance -= tournament.Deposit;
            tournament.Prize += tournament.Deposit;

            tournament.Users.Add(user);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: Tournament/4/finish
        // why don't FinishTournament returns the winner id?
        [HttpPost("{id}/finish")]
        public async Task<ActionResult> FinishTournament(Guid id)
        {
            Tournament tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            User winner = GetWinner(tournament);

            winner.Balance += tournament.Prize;

            return NoContent();
        }

        private User GetWinner(Tournament tournament)
        {
            var rand = new Random();
            int winnerIndex = rand.Next(0, tournament.Users.Count() - 1);
            return tournament.Users[winnerIndex];
        }
    }
}
