using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShareMe.DataAccessLayer.Entity
{
    public class Tag : Entity
    {

        [Required]
        public string Name { get; set; }

        public IList<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
