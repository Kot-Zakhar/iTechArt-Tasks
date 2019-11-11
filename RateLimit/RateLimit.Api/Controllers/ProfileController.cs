using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RateLimit.Api.Models;
using RateLimit.Api.Services;

namespace RateLimit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;
        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("page")]
        public ActionResult<IEnumerable<Profile>> Get(int profilePerPage, int pageIndex) 
        {
            return _profileService.GetPage(pageIndex, profilePerPage).ToArray();
        }
    }
}