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
        public UserRepository(DbContext context) : base(context) {}

        public User GetByEmail(string email)
        {
            return (from user in base.typeSet
                    where user.Email == email
                    select user).FirstOrDefault();
        }

        public IEnumerable<UserInfo> GetInfos()
        {
            return (from user in base.typeSet
                    select user)
                    .Select(user => new UserInfo(user));
        }
        
        public IEnumerable<UserInfo> GetInfosSortedByName()
        {
            return GetInfos().OrderBy(userInfo => userInfo.Name);
        }
    }
}
