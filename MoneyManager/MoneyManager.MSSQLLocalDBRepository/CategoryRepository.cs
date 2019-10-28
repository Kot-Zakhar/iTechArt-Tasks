﻿using System;
using System.Data.Entity;
using System.Linq;
using MoneyManager.Entity;
using MoneyManager.Repository;

namespace MoneyManager.MSSQLLocalDBRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
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