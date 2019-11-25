using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PermissionsAttribute.WebApp.Models;

/** 
    This controller is created for adding and removing users' claims.
    DISCLAIMER:
    Educational purposes only!
 */

namespace PermissionsAttribute.WebApp.Controllers
{
    public class MyClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private UserManager<IdentityProfile> _userManager;
        public ClaimController(UserManager<IdentityProfile> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IList<Claim>> GetUserClaims(string id)
        {
            return await _userManager.GetClaimsAsync(await _userManager.FindByIdAsync(id));
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<IdentityProfile>> AddClaims(string id, [FromBody] IEnumerable<MyClaim> claims)
        {
            IdentityProfile user = await _userManager.FindByIdAsync(id);
            await _userManager.AddClaimsAsync(user, claims.Select(c => new Claim(c.Type, c.Value)));
            return user;
        }
    }
}