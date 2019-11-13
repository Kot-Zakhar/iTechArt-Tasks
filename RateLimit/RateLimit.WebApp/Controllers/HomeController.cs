using System;
using System.Diagnostics;
using System.Linq;
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

        private string getFilterAndSortQueryFromFilter(FilterModel<Profile> filter)
        {
            string query = "";
            if (filter.Filter)
            {
                query += "&filterfield=" + filter.FilterField;
                query += filter.FilterValues.Aggregate("", (result, value) => "&filterValues[]=" + result);
            }
            if (!String.IsNullOrEmpty(filter.Sort))
            {
                query += "&sort=" + filter.Sort;
                query += "&sortField=" + filter.SortField;
            }
            return query;
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
        public IActionResult Profile([FromQuery]FilterModel<Profile> profileFilter)
        {
            FilteredResult<Profile> result = _profileService.GetFiltered(profileFilter);
            string filterAndSort = getFilterAndSortQueryFromFilter(profileFilter);
            result.Url = "/home/profile";
            result.NextPage = result.Next ? (result.Url + "?page=" + (profileFilter.Page + 1) + "&pageSize=" + profileFilter.PageSize + filterAndSort) : null;
            result.PreviousPage = result.Previous ? (result.Url + "?page=" + (profileFilter.Page - 1) + "&pageSize=" + profileFilter.PageSize + filterAndSort) : null;
            return View(result);
        }

        public IActionResult Index()
        {
            return Redirect("/Home/Profile");
        }
    }
}
