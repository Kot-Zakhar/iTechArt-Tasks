using Bogus;
using ShareMe.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.Faker
{
    public static class CommentFaker
    {
        public static Faker<Comment> GetCommentFaker()
        {
            return new Faker<Comment>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.ChildComments, f => new List<Comment>());
        }
    }
}
