using Bogus;
using ShareMe.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.Faker
{
    public static class TagFaker
    {
        public static Faker<Tag> GetTagFaker()
        {
            return new Faker<Tag>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Name, f => f.Lorem.Word());
        }

        public static Tag Generate()
        {
            return GetTagFaker().Generate();
        }

        public static IEnumerable<Tag> GenerateRange(int amount = 100)
        {
            return GetTagFaker().GenerateLazy(amount);
        }
    }
}
