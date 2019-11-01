using System.Linq;
using MoneyManager.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace MoneyManager.DataAccess.UnitOfWork.Repository
{
    public class UserRepository : Repository<User>
    {
        protected DbSet<User> UserSet { get => TypeSet; }
        public UserRepository(DbContext context) : base(context) {}

        /// <summary>
        /// Task: "Write a request to return the user by email"
        /// </summary>
        public User GetByEmail(string email)
        {
            return UserSet.FirstOrDefault(u => u.Email == email);
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
