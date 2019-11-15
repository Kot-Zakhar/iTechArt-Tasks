using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomJsonFormatter.Api.Models;

namespace CustomJsonFormatter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        // GET: api/Profile&amount=10
        [HttpGet(Name = "GetProfiles")]
        public IEnumerable<Profile> Get(int Amount = 10)
        {
            return Enumerable.Range(0, Amount).Select(i => new Profile());
        }

        // GET: api/Profile/Guid
        [HttpGet("{id}", Name = "GetProfileById")]
        public Profile Get(Guid id)
        {
            return new Profile(id);
        }
    }
}
