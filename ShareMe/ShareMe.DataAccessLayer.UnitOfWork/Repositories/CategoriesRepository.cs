using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.DataAccessLayer.UnitOfWork.Repositories
{
    public class CategoriesRepository : Repository<Category>
    {
        public CategoriesRepository(DbContext context) : base(context)
        {
        }
    }
}
