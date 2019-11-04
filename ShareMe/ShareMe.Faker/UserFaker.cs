using ShareMe.DataAccessLayer.Entity;
using System;
using Bogus;
using System.Collections.Generic;

namespace ShareMe.Faker
{
    public static class UserFaker
    {
        public static Faker<User> GetUserFaker()
        {
            return new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Name, f => f.Name.FirstName())
                .RuleFor(u => u.Surname, f => f.Name.LastName())
                .RuleFor(u => u.Username, (f, u) => f.Internet.UserName(u.Name, u.Surname));
        }

        public static User Generate()
        {
            return GetUserFaker().Generate();
        }

        public static IEnumerable<User> GenerateRange(int amount = 100)
        {
            return GetUserFaker().GenerateLazy(amount);
        }
    }
}
