using ShareMe.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Bogus;

namespace ShareMe.Faker
{
    public static class PostFaker
    {
        public static Faker<Post> GetPostFaker()
        {
            return new Faker<Post>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Author, f => UserFaker.Generate())
                .RuleFor(p => p.Category, f => CategoryFaker.Generate())
                .RuleFor(p => p.Content, f => f.Lorem.Paragraphs(3))
                .RuleFor(p => p.CreationDate, f => f.Date.Past())
                .RuleFor(p => p.ImageUrl, f => f.Image.PicsumUrl())
                .RuleFor(p => p.Rating, f => f.Random.Int(0, 100))
                .RuleFor(p => p.Title, f => f.Lorem.Sentence())
                .RuleFor(p => p.PostTags, (f, p) => {
                    IEnumerable<Tag> tags = TagFaker.GenerateRange(5);
                    var result = new List<PostTag>();
                    foreach (var tag in tags)
                        result.Add(new PostTag()
                            {
                                Post = p,
                                PostId = p.Id,
                                Tag = tag,
                                TagId = tag.Id
                            });
                    return result;
                })
                .RuleFor(p => p.URI, (f, p) => p.Id.ToString());
        }

        public static Post Generate()
        {
            return GetPostFaker().Generate();
        }

        public static Post Generate(Guid id)
        {
            return GetPostFaker()
                .RuleFor(p => p.Id, () => id)
                .Generate();
        }

        public static IEnumerable<Post> GenerateRange(int amount = 100)
        {
            return GetPostFaker().GenerateLazy(amount);
        }
    }
}
