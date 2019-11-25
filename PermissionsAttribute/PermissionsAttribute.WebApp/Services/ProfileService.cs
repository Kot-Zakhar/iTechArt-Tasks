using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PermissionsAttribute.WebApp.Models;

namespace PermissionsAttribute.WebApp.Services
{
    public class ProfileService
    {
        private UserManager<IdentityProfile> _userManager;

        public ProfileService(UserManager<IdentityProfile> userManager)
        {
            _userManager = userManager;
        }

        public IQueryable<Profile> GetAll()
        {
            return _userManager.Users
                .Select(u => new Profile(u));
        }

        public async Task<Profile> GetById(Guid id)
        {
            IdentityProfile user = await _userManager.FindByIdAsync(id.ToString());
            return new Profile(user);
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

        public async Task<bool> Create(Profile profile, string password)
        {
            var user = new IdentityProfile(profile.UserName)
            {
                Email = profile.Email,
                PhoneNumber = profile.PhoneNumber,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserName = profile.UserName,
                Birthday = profile.Birthday,
            };
            return (await _userManager.CreateAsync(user, password)).Succeeded;
        }

        public async Task<bool> Update(Profile profile)
        {
            IdentityProfile user = await _userManager.FindByIdAsync(profile.Id.ToString());
            if (user == null)
                return false;
            user.Email = profile.Email ?? user.Email;
            user.Birthday = profile.Birthday ?? user.Birthday;
            user.FirstName = profile.FirstName ?? user.FirstName;
            user.LastName = profile.LastName ?? user.LastName;
            user.PhoneNumber = profile.PhoneNumber ?? user.PhoneNumber;
            user.UserName = profile.UserName ?? user.UserName;
            return (await _userManager.UpdateAsync(user)).Succeeded;
        }

        public async Task<bool> DeleteById(Guid id)
        {
            IdentityProfile user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return false;
            return (await _userManager.DeleteAsync(user)).Succeeded;
        }
    }
}
