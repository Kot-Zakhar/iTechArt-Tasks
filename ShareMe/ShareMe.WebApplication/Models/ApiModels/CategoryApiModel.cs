using ShareMe.DataAccessLayer.Entity;
using ShareMe.WebApplication.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.Models.ApiModels
{
    public class CategoryApiModel : ApiModel
    {
        public Guid? ParentCategoryId { get; set; }
        public string Name { get; set; }
        public List<Guid> ChildCategoriesIds { get; set; } = new List<Guid>();

        public CategoryApiModel() { }

        public CategoryApiModel(Category category) : base(category)
        {
            ParentCategoryId = category.ParentCategory?.Id;
            Name = category.Name;
            ChildCategoriesIds.AddRange(category.ChildCategories.Select(c => c.Id));
        }
    }
}
