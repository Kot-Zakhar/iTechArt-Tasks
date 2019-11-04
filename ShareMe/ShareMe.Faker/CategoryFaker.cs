using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using ShareMe.DataAccessLayer.Entity;

namespace ShareMe.Faker
{
    public static class CategoryFaker
    {
        public static Faker<Category> GetCategoryFaker()
        {
            return new Faker<Category>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => f.Lorem.Word())
                .RuleFor(c => c.ChildCategories, () => new List<Category>())
                .RuleFor(c => c.Posts, () => new List<Post>());
        }

        public static Category Generate()
        {
            return GetCategoryFaker().Generate();
        }

        public static IEnumerable<Category> GenerateRange(int amount = 100)
        {
            return GetCategoryFaker().GenerateLazy(amount);
        }

    }
}
