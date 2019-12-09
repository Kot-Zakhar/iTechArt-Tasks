using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialTournamentService.SocialTournamentServiceDbContext.Models;
using SocialTournamentService.SocialTournamentServiceDbContext;

namespace SocialTournamentService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TournamentServiceDbContext _context;

        public UserController(TournamentServiceDbContext context)
        {
            _context = context;
        }

        // GET: User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, [FromBody] User userData)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            user.Name = userData.Name ?? user.Name;
            user.Balance = userData.Balance == 0 ? user.Balance : userData.Balance;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: User
        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody] User userData)
        {
            var user = new User()
            {
                Name = userData.Name,
                Balance = userData.Balance
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostUser", new { id = user.Id });
        }

        // DELETE: User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: User/4/take
        [HttpPost("{id}/take")]
        public async Task<ActionResult> TakePoints(Guid id, [FromForm] int points)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Balance < points)
            {
                return BadRequest(new {message = "Cannot take points: not enough points on balance."});
            }

            if (points <= 0)
            {
                return BadRequest(new {message = "Only positive points' amount allowed."});
            }

            user.Balance -= points;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: User/4/fund
        [HttpPost("{id}/fund")]
        public async Task<ActionResult> AddPoints(Guid id, [FromForm] int points)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (points <= 0)
            {
                return BadRequest(new {message = "Only positive points' amount allowed."});
            }

            user.Balance += points;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
