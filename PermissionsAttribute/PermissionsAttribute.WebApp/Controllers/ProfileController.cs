using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PermissionsAttribute.WebApp.Models;
using PermissionsAttribute.WebApp.Services;

namespace PermissionsAttribute.WebApp.Controllers
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

        // GET: Profile/5
        [HttpGet("{id}")]
        public ActionResult Details(Guid id)
        {
            //return View();
            return NotFound();
        }

        // GET: Profile/all
        [HttpGet("all")]
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

    }
}