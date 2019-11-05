using ShareMe.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using Bogus;

namespace ShareMe.Faker
{
    public static class DbFaker
    {
        public static int PostsAmount = 50;
        public static int RootCategoryAmount = 5;
        public static int ChildCategoryAmount = 20;
        public static int RootCommentsAmount = 10;
        public static int ChildCommentsAmount = 40;
        public static int UsersAmount = 100;
        public static int TagsAmount = 10;
        public static int TagsPerPost = 4;

        public static List<Post> Posts = new List<Post>();
        public static List<Category> Categories = new List<Category>();
        public static List<Comment> Comments = new List<Comment>();
        public static List<User> Users = new List<User>();
        public static List<Tag> Tags = new List<Tag>();


        public static void GenerateUsers()
        {
            Users = UserFaker.GetUserFaker().GenerateLazy(UsersAmount).ToList();
        }

        public static void GenerateCategories()
        {
            Categories = CategoryFaker.GetCategoryFaker().GenerateLazy(RootCategoryAmount).ToList();
            Categories.AddRange(
                CategoryFaker.GetCategoryFaker()
                    .RuleFor(c => c.ParentCategory, (f, c) =>
                    {
                        Category parentCategory = Categories[f.Random.Int(0, Categories.Count - 1)];
                        parentCategory.ChildCategories.Add(c);
                        return parentCategory;
                    })
                    .GenerateLazy(ChildCategoryAmount)
                    .ToList()
            );
        }

        public static void GeneratePostsFromUsersWithCategories()
        {
            Posts = PostFaker.GetPostFaker()
                .RuleFor(p => p.Author, (f, p) => {
                    User user = Users[f.Random.Int(0, Users.Count - 1)];
                    user.Posts.Add(p);
                    return user;
                })
                .RuleFor(p => p.Category, (f, p) =>
                {
                    Category category = Categories[f.Random.Int(0, Categories.Count - 1)];
                    category.Posts.Add(p);
                    return category;
                })
                .GenerateLazy(PostsAmount).ToList();
        }

        public static void GenerateCommentsForPostsFromUsers()
        {
            Faker<Comment> commentFaker = CommentFaker.GetCommentFaker()
                .RuleFor(c => c.Author, (f, c) => {
                    User user = Users[f.Random.Int(0, Users.Count - 1)];
                    user.Comments.Add(c);
                    return user;
                })
                .RuleFor(c => c.Post, (f, c) =>
                {
                    Post post = Posts[f.Random.Int(0, Posts.Count - 1)];
                    post.Comments.Add(c);
                    return post;
                });

            Comments = commentFaker.GenerateLazy(RootCommentsAmount).ToList();

            Comments.AddRange(
                commentFaker
                .RuleFor(c => c.ParentComment, (f, c) => {
                    var parentComment = Comments[f.Random.Int(0, Comments.Count - 1)];
                    parentComment.ChildComments.Add(c);
                    return parentComment;
                })
                .GenerateLazy(ChildCommentsAmount)
            );
                  
        }

        public static void GenerateTags()
        {
            Tags = TagFaker.GetTagFaker().GenerateLazy(TagsAmount).ToList();
        }

        public static void AssignTagsToPosts()
        {
            var random = new Random();
            foreach(var post in Posts)
            {
                for (var i = post.PostTags.Count; i < TagsPerPost; i++)
                {
                    Tag tag = Tags[random.Next(0, Tags.Count - 1)];
                    if (post.PostTags.Any(postTag => postTag.TagId == tag.Id))
                    {
                        i--;
                    } else
                    {
                        post.PostTags.Add(new PostTag()
                        {
                            Post = post,
                            PostId = post.Id,
                            Tag = tag,
                            TagId = tag.Id
                        });
                    }
                }
            }
        }

        public static void Generate()
        {
            bool assignTagsToPosts = false;
            if (Users.Count == 0)
                GenerateUsers();
            if (Categories.Count == 0)
                GenerateCategories();
            if (Posts.Count == 0)
            {
                GeneratePostsFromUsersWithCategories();
                assignTagsToPosts = true;
            }
            if (Comments.Count == 0)
                GenerateCommentsForPostsFromUsers();
            if (Tags.Count == 0)
                GenerateTags();
            if (assignTagsToPosts)
                AssignTagsToPosts();
        }

        public static void Reset()
        {
            Posts.Clear();
            Categories.Clear();
            Comments.Clear();
            Users.Clear();
            Tags.Clear();
        }
    }
}
