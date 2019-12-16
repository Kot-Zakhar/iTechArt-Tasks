using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialTournamentService.Api.ApiModels;
using SocialTournamentService.Api.Services;
using SocialTournamentService.SocialTournamentServiceDbContext;
using SocialTournamentService.SocialTournamentServiceDbContext.Models;

namespace SocialTournamentService.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TournamentService _tournamentService;
        private readonly PointsService _pointsService;

        public TournamentController(UserService userService, TournamentService tournamentService, PointsService pointsService)
        {
            _userService = userService;
            _tournamentService = tournamentService;
            _pointsService = pointsService;
        }

        // GET: Tournament
        [HttpGet]
        public async Task<ActionResult<IList<TournamentApiModel>>> GetTournaments()
        {
            return (await _tournamentService.GetTournaments().ToListAsync()).Select(t => new TournamentApiModel(t)).ToList();
        }

        // GET: Tournament/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentApiModel>> GetTournament(Guid id)
        {
            Tournament tournament = await _tournamentService.GetByIdAsync(id);
            
            if (tournament == null)
            {
                return NotFound();
            }

            return new TournamentApiModel(tournament);
        }

        // PUT: Tournament/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTournament(Guid id, [FromBody] TournamentApiModel data)
        {
            var tournamentData = new Tournament()
            {
                Id = id,
                Name = data.Name,
                Deposit = data.Deposit
            };

            Tournament newTournament = await _tournamentService.UpdateTournamentAsync(tournamentData);

            return NoContent();
        }

        // POST: Tournament
        [HttpPost]
        public async Task<ActionResult<object>> CreateTournament([FromBody] TournamentApiModel tournamentData)
        {
            var tournament = new Tournament()
            {
                Name = tournamentData.Name,
                Deposit = tournamentData.Deposit
            };

            Tournament newTournament = await _tournamentService.CreateAsync(tournament);

            return new { id = newTournament.Id };
        }

        // DELETE: Tournament/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTournament(Guid id)
        {
            Tournament tournament = await _tournamentService.GetByIdAsync(id);
            if (!await _tournamentService.TournamentExistsAsync(id))
            {
                return NotFound();
            }

            await _tournamentService.Delete(id);

            return NoContent();
        }

        // POST: Tournament/4/join
        [HttpPost("{id}/join")]
        public async Task<ActionResult> JoinTournament(Guid id, [FromBody] Guid userId)
        {
            if (!await _tournamentService.TournamentExistsAsync(id))
                return NotFound("Such tournament doesn't exist.");
            if (!await _userService.UserExistsAsync(id))
                return NotFound("Such user doesn't exist.");

            bool result = await _tournamentService.JoinUserAsync(id, userId);

            if (!result)
            {
                return BadRequest("This user can't join current tournament.");
            }

            return NoContent();
        }

        // POST: Tournament/4/finish
        // why don't FinishTournament returns the winner id?
        [HttpPost("{id}/finish")]
        public async Task<ActionResult> FinishTournament(Guid id)
        {
            if (!await _tournamentService.TournamentExistsAsync(id))
            {
                return NotFound();
            }

            await _tournamentService.FinishTournamentAsync(id);

            return NoContent();
        }

    }
}
