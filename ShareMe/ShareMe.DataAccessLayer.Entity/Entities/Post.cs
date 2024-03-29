﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShareMe.DataAccessLayer.Entity
{
    public class Post : Entity
    {
        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [ForeignKey("AuthorId")]
        [Required]
        public virtual User Author { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreationDate { get; set; }

        public int Rating { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string Content { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DataType(DataType.Url)]
        [Required]
        public string URI { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual IList<PostTag> PostTags { get; set; } = new List<PostTag>();
        public virtual IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}
