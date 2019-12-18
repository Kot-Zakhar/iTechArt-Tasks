using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialTournamentService.Api.Services;
using SocialTournamentService.SocialTournamentServiceDbContext.Models;
using SocialTournamentService.SocialTournamentServiceDbContext;

namespace SocialTournamentService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PointsService _pointsService;
        private readonly UserService _userService;

        public UserController(TournamentServiceDbContext context, PointsService pointsService, UserService userService)
        {
            _pointsService = pointsService;
            _userService = userService;
        }

        // GET: User
        [HttpGet]
        public async Task<ActionResult<IList<User>>> GetUsers()
        {
            return await _userService.GetUsers().ToListAsync();
        }

        // GET: User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            User user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User data)
        {
            data.Id = id;
            User newUser = await _userService.UpdateUserAsync(data);
            
            if (newUser == null)
                return BadRequest();


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
            await _userService.CreateUserAsync(user);
            return CreatedAtAction("PostUser", new { id = user.Id });
        }

        // DELETE: User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            if (!(await _userService.UserExistsAsync(id)))
            {
                return NotFound();
            }

            await _userService.DeleteByIdAsync(id);

            return NoContent();
        }

        // POST: User/4/take
        [HttpPost("{id}/take")]
        public async Task<ActionResult> TakePoints(Guid id, [FromForm] int points)
        {
            User user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (points <= 0)
            {
                return BadRequest(new { message = "Only positive amount of points allowed." });
            }

            bool result = _pointsService.TakePoints(user, points);

            if (!result)
            {
                return BadRequest(new {message = "Can't withdraw points."});
            }

            return NoContent();
        }

        // POST: User/4/fund
        [HttpPost("{id}/fund")]
        public async Task<ActionResult> AddPoints(Guid id, [FromForm] int points)
        {
            User user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (points <= 0)
            {
                return BadRequest(new { message = "Only positive amount of points allowed." });
            }

            _pointsService.AddPoints(user, points);

            return NoContent();
        }
    }
}
