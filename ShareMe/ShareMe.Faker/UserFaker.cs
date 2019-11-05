using ShareMe.DataAccessLayer.Entity;
using System;
using Bogus;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ShareMe.Faker
{
    public static class UserFaker
    {
        public static int saltLength = 1024;
        public static Faker<User> GetUserFaker()
        {
            return new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Salt, f => Convert.ToBase64String(f.Random.Bytes(saltLength)))
                .RuleFor(u => u.Hash, (f, u) => Convert.ToBase64String(
                    SHA512.Create().ComputeHash(
                        Encoding.Unicode.GetBytes(u.Salt + u.Email)
                    )
                ))
                .RuleFor(u => u.Name, f => f.Name.FirstName())
                .RuleFor(u => u.Surname, f => f.Name.LastName())
                .RuleFor(u => u.Username, (f, u) => f.Internet.UserName(u.Name, u.Surname))
                .RuleFor(u => u.Comments, () => new List<Comment>())
                .RuleFor(u => u.Posts, () => new List<Post>());
        }
    }
}
