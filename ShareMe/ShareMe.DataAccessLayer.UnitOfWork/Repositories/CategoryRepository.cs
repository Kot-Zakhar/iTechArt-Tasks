using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.DataAccessLayer.UnitOfWork.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        private DbSet<Category> CategoryTypeSet { get => typeSet; }
        public CategoryRepository(DbContext context) : base(context) { }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await CategoryTypeSet.SingleAsync(c => c.Name == name);
        }

        public IQueryable<Category> GetAllRootCategories()
        {
            return CategoryTypeSet.Where(category => category.ParentCategory == null);
        }
    }
}
