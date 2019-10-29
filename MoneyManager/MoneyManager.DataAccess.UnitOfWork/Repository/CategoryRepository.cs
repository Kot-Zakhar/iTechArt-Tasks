using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccess.Entity;

namespace MoneyManager.DataAccess.UnitOfWork.Repository
{
    public class CategoryRepository : Repository<Category>
    {
        protected DbSet<Category> CategorySet { get => typeSet; }

        public CategoryRepository(DbContext context) : base(context) {}

        public IQueryable<Category> GetCategoriesSorted(CategoryType type)
        {
            return CategorySet.Where(c => c.Type == type).OrderBy(c => c.Name);
        }
    }
}
