using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShareMe.DataAccessLayer.Entity
{
    public class Comment : Entity
    {
        public Comment() : base() { }

        public Comment(Guid id) : base(id) { }

        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [ForeignKey("ParentId")]
        public Comment ParentComment { get; set; }

        public IList<Comment> ChildComments { get; set; } = new List<Comment>();

        [ForeignKey("AuthorId")]
        public User Author { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreationTime { get; set; }
    }
}
