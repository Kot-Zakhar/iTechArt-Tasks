using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccess.Entity;

namespace MoneyManager.DataAccess.UnitOfWork.Repository
{
    public class CategoryRepository : Repository<Category>
    {
        protected DbSet<Category> CategorySet { get => typeSet; }

        public CategoryRepository(DbContext context) : base(context) {}

        public IQueryable<CategoryInfo> GetParentCategories(CategoryType categoryType)
        {
            return (from category in CategorySet
                    where String.IsNullOrEmpty(category.ParentCategory.Name) && category.Type == categoryType
                    select category).Select(category => new CategoryInfo(category));
        }
    }
}
