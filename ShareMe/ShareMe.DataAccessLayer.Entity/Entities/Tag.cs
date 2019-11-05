using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace ShareMe.DataAccessLayer.Entity
{
    public class Tag : Entity
    {

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public IList<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
