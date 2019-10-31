using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShareMe.DataAccessLayer.Entity
{
    public class PostTag
    {
        [Required]
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [Required]
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
