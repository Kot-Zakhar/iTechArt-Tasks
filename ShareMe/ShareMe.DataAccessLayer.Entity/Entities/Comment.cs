using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace ShareMe.DataAccessLayer.Entity
{
    public class Comment : Entity
    {
        public Comment() : base() { }

        public Comment(Guid id) : base(id) { }

        [JsonIgnore]
        public Post Post { get; set; }

        [NotMapped]
        public Guid PostId { get => Post.Id; }

        [JsonIgnore]
        public Comment ParentComment { get; set; }

        [NotMapped]
        public Guid ParentCommentId { get => ParentComment.Id; }

        [JsonIgnore]
        public IList<Comment> ChildComments { get; set; } = new List<Comment>();

        [JsonIgnore]
        public User Author { get; set; }

        [NotMapped]
        public Guid AuthorId { get => Author.Id; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationTime { get; set; }
    }
}
