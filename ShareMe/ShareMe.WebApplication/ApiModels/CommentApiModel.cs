using System;
using System.Collections.Generic;
using System.Linq;
using ShareMe.DataAccessLayer.Entity;

namespace ShareMe.WebApplication.ApiModels
{
    public class CommentApiModel : ApiModel
    {
        public Guid PostId { get; set; }
        public Guid ParentCommentId { get; set; }
        public Guid AuthorId { get; set; }
        public List<Guid> ChildCommentIds { get; set; } = new List<Guid>();
        public DateTime CreationTime { get; set; }
        public string Content { get; set; }

        public CommentApiModel() { }
        public CommentApiModel(Comment comment) : base(comment)
        {
            PostId = comment.Post.Id;
            ParentCommentId = comment.ParentComment.Id;
            AuthorId = comment.Author.Id;
            ChildCommentIds.AddRange(comment.ChildComments.Select(c => c.Id));
            CreationTime = comment.CreationTime;
            Content = comment.Content;
        }
    }
}