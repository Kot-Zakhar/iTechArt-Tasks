using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using MoneyManager.Entity;
using MoneyManager.Repository;

namespace MoneyManager.MSSQLLocalDBRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        protected DbSet<User> UserSet { get => typeSet; }
        public UserRepository(DbContext context) : base(context) {}

        /// <summary>
        /// Task: "Write a request to return the user by email"
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetByEmail(string email)
        {
            return (from user in UserSet
                    where user.Email == email
                    select user).FirstOrDefault();
        }

        /// <summary>
        /// Task: "Write a query to return the user list sorted by the user’s name."
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserInfo> GetInfos()
        {
            return (from user in UserSet
                    select user)
                    .Select(user => new UserInfo(user))
                    .OrderBy(userInfo => userInfo.Name);
        }
    }
}
