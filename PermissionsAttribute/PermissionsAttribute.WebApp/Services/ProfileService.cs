using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using PermissionsAttribute.WebApp.Models;

namespace PermissionsAttribute.WebApp.Services
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

        public GridResult<Profile> GetFiltered(GridModel<Profile> grid)
        {
            Func<string, Profile, object> getPropertyValue = (fieldName, profile) => typeof(Profile).GetProperty(fieldName).GetValue(profile);

            Thread.Sleep(1000);
            IQueryable<Profile> allValues = GetAll();

            if (grid.Filter)
                allValues = allValues.Where(profile => grid.FilterValues.Any(value => value == getPropertyValue(grid.FilterField, profile).ToString()));

            if (!string.IsNullOrEmpty(grid.Sort))
                if (grid.Sort.ToUpper() == "ASC")
                    allValues = allValues.OrderBy(profile => getPropertyValue(grid.SortField, profile));
                else
                    allValues = allValues.OrderByDescending(profile => getPropertyValue(grid.SortField, profile));

            IQueryable<Profile> pagedValues = allValues.Skip(grid.Page * grid.PageSize).Take(grid.PageSize);

            var result = new GridResult<Profile>()
            {
                Values = pagedValues,
                Next = allValues.Skip((grid.Page + 1) * grid.PageSize).Take(grid.PageSize).Any(),
                Previous = grid.Page != 0 && allValues.Skip((grid.Page - 1) * grid.PageSize).Take(grid.PageSize).Any(),
                Page = grid.Page,
                PageSize = grid.PageSize
            };
            return result;
        }
    }
}
