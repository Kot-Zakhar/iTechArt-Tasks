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
    public interface ICategoryRepository : IRepository<Entity.Category>
    {
        //public IEnumerable<CategoryInfo> GetCategoryInfosByType();

    }
}
