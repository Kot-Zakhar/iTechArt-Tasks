using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RateLimit.WebApp.Filters;
using RateLimit.WebApp.Models;
using RateLimit.WebApp.Services;

namespace RateLimit.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProfileService _profileService;

        public HomeController(ProfileService profileService)
        {
            _profileService = profileService;
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


        // no parameters are strictly required
        // Examples:
        //   - sort by username:  /home/profile?sortField=username
        //   - show second page (4 records each) of Bobs and Alices sorted by Birthday descending: 
        //     /home/profile?
        //           page=2&
        //           pageSize=4&
        //           filterField=firstName&
        //           filterValues=Alice&
        //           filterValues=Bob&
        //           sort=desc&
        //           sortField=birthday
        [ConcurrentRequestsLimitFilter(10)]
        public IActionResult Profile([FromQuery]GridModel<Profile> profileGrid)
        {
            GridResult<Profile> result = _profileService.GetFiltered(profileGrid);
            AddPageLinks(ref result, profileGrid);
            return View(result);
        }

        private void AddPageLinks(ref GridResult<Profile> result, GridModel<Profile> profileGrid)
        {
            string filterAndSort = GetQueryStringFromFilter(profileGrid);
            result.Url = "/home/profile";

            Func<int, string> GetQueryString = (int pageInc) => "?page=" + (profileGrid.Page + pageInc) + "&pageSize=" + profileGrid.PageSize + "&" + filterAndSort;

            result.NextPageUrl = result.Next ? result.Url + GetQueryString(1) : null;
            result.PreviousPageUrl = result.Previous ? result.Url + GetQueryString(-1) : null;
        }

        public IActionResult Index()
        {
            return Redirect("/Home/Profile");
        }
    }
}
