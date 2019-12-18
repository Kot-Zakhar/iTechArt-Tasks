using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialTournamentService.SocialTournamentServiceDbContext;
using SocialTournamentService.SocialTournamentServiceDbContext.Models;

namespace SocialTournamentService.Api.Services
{
    public class UserService
    {
        private readonly TournamentServiceDbContext _context;

        public UserService(TournamentServiceDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetUsers()
        {
            return _context.Users;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.SingleAsync(u => u.Id == id);
        }

        public async Task<User> UpdateUserAsync(User userData)
        {
            User user = await GetByIdAsync(userData.Id);

            if (user == null)
                return null;

            user.Name = userData.Name ?? user.Name;
            user.Balance = userData.Balance == 0 ? user.Balance : userData.Balance;

            
            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            EntityEntry<User> entry = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            User user = await GetByIdAsync(id);
            if (user == null)
                return;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(Guid id)
        {
            return (await _context.Users.SingleAsync(u => u.Id == id)) != null;
        }
    }
}
