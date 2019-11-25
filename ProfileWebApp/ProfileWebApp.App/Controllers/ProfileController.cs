using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileWebApp.WebApp.Models;
using ProfileWebApp.WebApp.Services;
using ProfileWebApp.WebApp.Filter;

namespace ProfileWebApp.WebApp.Controllers
{
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        private readonly ProfileService _profileService;

        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
        }

        public ActionResult Index()
        {
            return Redirect("/Profile/All?page=0&pageSize=10");
        }

        // GET: Profile/Details/5
        [HttpGet("Details/{id}")]
        [HasPermission(Permission.GetProfileById)]
        public async Task<ActionResult> Details(Guid id)
        {
            Profile profile = await _profileService.GetById(id);
            if (profile == null)
                return NotFound();
            else
                return View("Details", profile);
        }

        // GET: Edit/5
        [HttpGet("Edit/{id}")]
        [HasPermission(Permission.UpdateProfile)]
        public async Task<ActionResult> Edit(Guid id)
        {
            Profile profile = await _profileService.GetById(id);
            if (profile == null)
                return NotFound();
            else
                return View("Edit", profile);
        }

        // POST: Edit/id
        [HttpPost("Update/{id}")]
        [HasPermission(Permission.UpdateProfile)]
        public async Task<ActionResult> Update(Guid id, [FromForm] Profile profile)
        {
            if (await _profileService.Update(profile))
                return Redirect("/Profile/Index");
            else
                return BadRequest();
        }

        // GET: Delete/id
        [HttpGet("Delete/{id}")]
        [HasPermission(Permission.DeleteProfile)]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _profileService.DeleteById(id))
                return Redirect("/Profile/Index");
            else
                return BadRequest();

        }

        // GET: Profile/all
        [HttpGet("all")]
        [HasPermission(Permission.GetProfiles)]
        public ActionResult All([FromQuery]GridModel<Profile> gridModel)
        {
            GridResult<Profile> result = _profileService.GetFiltered(gridModel);
            AddPageLinks(ref result, gridModel);
            return View("Profiles", result);
        }

        private string GetQueryStringFromFilter(GridModel<Profile> grid)
        {
            StringBuilder query = new StringBuilder();
            if (grid.Filter)
            {
                query.Append("filterfield=" + grid.FilterField);
                query.Append(grid.FilterValues.Aggregate("", (result, value) => "&filterValues[]=" + result));
            }
            if (!String.IsNullOrEmpty(grid.Sort))
            {
                if (query.Length != 0)
                    query.Append("&");
                query.Append("&sort=" + grid.Sort);
                query.Append("&sortField=" + grid.SortField);
            }
            return query.ToString();
        }

        private void AddPageLinks(ref GridResult<Profile> result, GridModel<Profile> profileGrid)
        {
            string filterAndSort = GetQueryStringFromFilter(profileGrid);
            result.Url = "/Profile/All";

            Func<int, string> GetQueryString = (int pageInc) => "?page=" + (profileGrid.Page + pageInc) + "&pageSize=" + profileGrid.PageSize + "&" + filterAndSort;

            result.NextPageUrl = result.Next ? result.Url + GetQueryString(1) : null;
            result.PreviousPageUrl = result.Previous ? result.Url + GetQueryString(-1) : null;
        }


        // POST: profile
        [HttpPost]
        [HasPermission(Permission.AddProfile)]
        public async Task<ActionResult> Create([FromBody] Profile profile)
        {
            string password = profile.Email;
            if (await _profileService.Create(profile, password))
                return Ok();
            else
                return NotFound();
        }


    }
}