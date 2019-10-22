using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.RandomGenerator
{
    public static class CategoryFaker
    {
        public static Category CreateCategory()
        {
            return new Category()
            {
                Name = Faker.Company.Name(),
                Type = (CategoryType)Faker.RandomNumber.Next(0, 1)
            };
        }

        public static Category CreateChildCategory(Category parentCategory)
        {
            var category = CreateCategory();
            category.Name = $"{parentCategory.Name}.{category.Name}";
            category.Type = parentCategory.Type;
            category.ParentCategory = parentCategory;
            return category;
        }

        // root is not included in tree if provided
        public static IEnumerable<Category> CreateCategoryTree(int childAmount = 2, int successorAmount = 3, Category root = null)
        {
            var tree = new List<Category>();

            if (root == null)
            {
                root = CreateCategory();
                tree.Add(root);
            }

            if (successorAmount == 0)
                return tree;

            for (int i = 0; i < childAmount; i++)
            {
                var node = CreateChildCategory(root);
                tree.Add(node);
                tree.AddRange(CreateCategoryTree(childAmount, successorAmount - 1, node));
            }

            return tree;
        }
    }
}
