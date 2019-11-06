using ShareMe.DataAccessLayer.Entity;
using System;

namespace ShareMe.WebApplication.ApiModels
{
    public class PostApiModel : ApiModel
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string URI { get; set; }
        public Guid? CategoryId { get; set; }

        public PostApiModel() { }

        public PostApiModel(Post post) : base(post)
        {
            Title = post.Title;
            AuthorId = post.Author.Id;
            CreationDate = post.CreationDate;
            Rating = post.Rating;
            Content = post.Content;
            ImageUrl = post.ImageUrl;
            URI = post.URI;
            CategoryId = post.Category?.Id;
        }
    }
}