using System;
using System.Linq;
using MoneyManager.Entity;

namespace MoneyManager.Repository
{
    public struct UserInfo
    {
        public Guid Id;
        public string Name;
        public string Email;

        public UserInfo(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
        }
    }
    public struct UserBalance
    {
        public UserInfo UserInfo;
        public double Balance;
    }
    public interface IUserRepository : IRepository<Entity.User>
    {
        /// <summary>
        /// Task: "Write a request to return the user by email"
        /// </summary>
        public Entity.User GetByEmail(string email);

        /// <summary>
        /// Used by GetInfosSortedByName();
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserInfo> GetInfos();

        /// <summary>
        /// Task: "Write a query to return the user list sorted by the user’s name."
        /// uses GetInfos()
        /// </summary>
        public IQueryable<UserInfo> GetInfosSortedByName();

    }
}
