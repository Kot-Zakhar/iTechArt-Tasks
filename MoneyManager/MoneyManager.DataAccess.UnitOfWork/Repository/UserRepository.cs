using System.Linq;
using MoneyManager.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace MoneyManager.DataAccess.UnitOfWork.Repository
{
    public class UserRepository : Repository<User>
    {
        protected DbSet<User> UserSet { get => typeSet; }
        public UserRepository(DbContext context) : base(context) {}

        /// <summary>
        /// Task: "Write a request to return the user by email"
        /// </summary>
        public User GetByEmail(string email)
        {
            return (from user in UserSet
                    where user.Email == email
                    select user).FirstOrDefault();
        }

        /// <summary>
        /// Task: "Write a query to return the user list sorted by the user’s name."
        /// </summary>
        public IQueryable<UserInfo> GetInfos()
        {
            return (from user in UserSet
                    select user)
                    .Select(user => new UserInfo(user))
                    .OrderBy(userInfo => userInfo.Name);
        }
    }
}
