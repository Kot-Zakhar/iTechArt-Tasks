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

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
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

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, [FromForm] string name)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || id != user.Id)
            {
                return BadRequest();
            }

            user.Name = name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult> PostUser([FromForm]string name)
        {
            var user = new User()
            {
                Name = name
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostUser", new { id = user.Id });
        }

        // DELETE: api/User/5
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

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
