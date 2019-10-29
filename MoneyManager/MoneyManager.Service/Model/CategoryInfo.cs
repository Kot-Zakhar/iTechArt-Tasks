using System;

namespace MoneyManager.Service.Model
{
    class CategoryInfo
    {
        public Guid Id { get; set;  }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public CategoryType Type { get; set; }

        public CategoryInfo(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ParentName = category.ParentCategory.Name;
            Type = category.Type;
        }
    }
}
