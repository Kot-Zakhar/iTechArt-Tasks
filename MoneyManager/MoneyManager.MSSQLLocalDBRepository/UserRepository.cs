using System;
using System.Collections.Generic;
using System.Text;
using MoneyManager.Repository;

namespace MoneyManager.MSSQLLocalDBRepository
{
    public class UserRepository : Repository<Entity.User>, IUserRepository
    {
    }
}
