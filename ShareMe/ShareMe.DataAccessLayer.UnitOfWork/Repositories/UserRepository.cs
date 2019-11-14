using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.DataAccessLayer.UnitOfWork.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
