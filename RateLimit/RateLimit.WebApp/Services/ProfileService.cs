using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using RateLimit.WebApp.Models;

namespace RateLimit.WebApp.Services
{
    public class ProfileService
    {
        private List<Profile> profiles = new List<Profile>();
        public readonly string FileName;

        public ProfileService(string fileName = "./Data/profiles.json")
        {
            FileName = fileName;
            string serializedProfiles = File.ReadAllText(Path.GetFullPath(FileName), System.Text.Encoding.UTF8);
            profiles.AddRange(JsonSerializer.Deserialize<IList<Profile>>(serializedProfiles));
        }

        public ProfileService(int amount)
        {
            profiles.AddRange(Enumerable.Range(0, amount).Select(index => new Profile()));
        }

        public IQueryable<Profile> GetAll()
        {
            return profiles.AsQueryable();
        }

        public FilteredResult<Profile> GetFiltered(FilterModel<Profile> filter)
        {
            Func<String, Profile, object> getPropertyValue = (fieldName, profile) => typeof(Profile).GetProperty(fieldName).GetValue(profile);

            Thread.Sleep(1000);
            IQueryable<Profile> allValues = GetAll();

            if (filter.Filter)
                allValues = allValues.Where(profile => filter.FilterValues.Any(value => value == getPropertyValue(filter.FilterField, profile).ToString()));

            if (!String.IsNullOrEmpty(filter.Sort))
                if (filter.Sort.ToUpper() == "ASC")
                    allValues = allValues.OrderBy(profile => getPropertyValue(filter.SortField, profile));
                else
                    allValues = allValues.OrderByDescending(profile => getPropertyValue(filter.SortField, profile));

            IQueryable<Profile> pagedValues = allValues.Skip(filter.Page * filter.PageSize).Take(filter.PageSize);

            var result = new FilteredResult<Profile>()
            {
                Values = pagedValues,
                Next = allValues.Skip((filter.Page + 1) * filter.PageSize).Take(filter.PageSize).Any(),
                Previous = filter.Page != 0 && allValues.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize).Any(),
                Page = filter.Page,
                PageSize = filter.PageSize
            };
            return result;
        }
    }
}
