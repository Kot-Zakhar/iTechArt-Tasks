using System;
using MoneyManager.Entity;

namespace MoneyManager.Repository
{
    public struct CategoryInfo
    {
        public Guid Id;
        public string Name;
        public string ParentName;

        public CategoryInfo(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ParentName = category.ParentCategory.Name;
        }
    }
    public struct CategoryAmountInfo
    {
        public CategoryInfo Category;
        public double Amount;
    }
    public interface ICategoryRepository : IRepository<Category>
    {
        public IEquatable<CategoryInfo> GetParentCategories(CategoryType categoryType);
    }
}
