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

        public Profile GetById(Guid id)
        {
            return profiles.Single(p => p.Id == id);
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

        public bool Create(Profile profile)
        {
            if (!profiles.Any(p => p.Id == profile.Id))
            {
                profiles.Add(profile);
                Save();
                return true;
            }
            return false;
        }

        public bool Update(Profile profile)
        {
            var index = profiles.FindIndex(p => p.Id == profile.Id);
            if (index >= 0)
            {
                profiles.RemoveAt(index);
                profiles.Add(profile);
                Save();
                return true;
            }
            return false;
        }

        public bool DeleteById(Guid id)
        {
            var result = profiles.Any(p => p.Id == id);
            profiles.RemoveAll(p => p.Id == id);
            return result;
        }

        private void Save()
        {
            string serializedProfiles = JsonSerializer.Serialize(profiles, typeof(IList<Profile>));
            File.WriteAllText(Path.GetFullPath(FileName), serializedProfiles, System.Text.Encoding.UTF8);
        }
    }
}
