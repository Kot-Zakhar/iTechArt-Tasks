using ShareMe.DataAccessLayer.Entity;
using ShareMe.WebApplication.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShareMe.WebApplication.Models.ApiModels
{
    public class PostApiModel : ApiModel
    {
        public string Title { get; set; }
        public UserApiModel Author { get; set; }
        public DateTime CreationDate { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string URI { get; set; }
        public CategoryApiModel Category { get; set; }
        public List<TagApiModel> Tags { get; set; } = new List<TagApiModel>();

        public PostApiModel() { }

        public PostApiModel(Post post) : base(post)
        {
            Title = post.Title;
            Author = new UserApiModel(post.Author);
            CreationDate = post.CreationDate;
            Rating = post.Rating;
            Content = post.Content;
            ImageUrl = post.ImageUrl;
            URI = post.URI;
            Category = new CategoryApiModel(post.Category);
            Tags.AddRange(post.PostTags.Select(pt => new TagApiModel(pt.Tag)));
        }
    }
}