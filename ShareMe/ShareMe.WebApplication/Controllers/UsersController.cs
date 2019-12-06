using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Services.Contracts;

namespace ShareMe.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IList<UserApiModel>>> GetUsers()
        {
            return (await _userService.GetAllAsync()).ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserApiModel>> GetUser(Guid id)
        {
            UserApiModel user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
