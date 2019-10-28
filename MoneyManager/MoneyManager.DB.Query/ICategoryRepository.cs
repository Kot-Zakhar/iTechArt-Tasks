using System;
using System.Linq;
using MoneyManager.Entity;

namespace MoneyManager.Repository
{
    public struct CategoryInfo
    {
        public Guid Id;
        public string Name;
        public string ParentName;
        public CategoryType Type;

        public CategoryInfo(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ParentName = category.ParentCategory.Name;
            Type = category.Type;
        }
    }
    public struct CategoryAmountInfo
    {
        public CategoryInfo Category;
        public double Amount;
    }
    public interface ICategoryRepository : IRepository<Category>
    {
        public IQueryable<CategoryInfo> GetParentCategories(CategoryType categoryType);
    }
}
