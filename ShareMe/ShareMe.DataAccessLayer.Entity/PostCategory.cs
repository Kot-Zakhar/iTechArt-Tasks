using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShareMe.DataAccessLayer.Entity
{
    public class PostCategory
    {
        [Required]
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [Required]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
