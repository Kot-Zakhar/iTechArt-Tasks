using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShareMe.DataAccessLayer.Entity
{
    public class PostTag
    {
        public Guid? PostId { get; set; }
        public Post Post { get; set; }

        public Guid? TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
