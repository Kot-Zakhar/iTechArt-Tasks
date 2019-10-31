using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShareMe.DataAccessLayer.Entity
{
    public class Comment : Entity
    {
        [Required]
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [ForeignKey("ParentId")]
        public Comment ParentComment { get; set; }

        [Required]
        [ForeignKey("AuthorId")]
        public User Author { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationTime { get; set; }
    }
}
