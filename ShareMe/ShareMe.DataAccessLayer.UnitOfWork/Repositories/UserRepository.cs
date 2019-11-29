using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using System.Threading.Tasks;

namespace ShareMe.DataAccessLayer.UnitOfWork.Repositories
{
    public class UserRepository : Repository<User>
    {
        private DbSet<User> UserTypeSet => typeSet;
        public UserRepository(DbContext context) : base(context) { }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await UserTypeSet.SingleAsync(u => u.Email == email);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await UserTypeSet.SingleAsync(u => u.Username == username);
        }
    }
}
