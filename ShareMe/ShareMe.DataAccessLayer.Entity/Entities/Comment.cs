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

        [Required]
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [ForeignKey("ParentId")]
        public Comment ParentComment { get; set; }

        public IList<Comment> ChildComments { get; set; } = new List<Comment>();

        [Required]
        [ForeignKey("AuthorId")]
        public User Author { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationTime { get; set; }
    }
}
