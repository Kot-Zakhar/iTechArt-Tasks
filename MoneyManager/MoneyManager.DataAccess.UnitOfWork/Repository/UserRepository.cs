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
        /// Sorting by user name
        /// </summary>
        public IQueryable<User> GetUsersSorted()
        {
            return UserSet.OrderBy(user => user.Name);
        }
    }
}
