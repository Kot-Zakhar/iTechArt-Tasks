using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace ShareMe.DataAccessLayer.Entity
{
    public class User : Entity
    {
        [DataType(DataType.Text)]
        [Required]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [JsonIgnore]
        public string Hash { get; set; }

        [Required]
        [JsonIgnore]
        public string Salt { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        [JsonIgnore]
        public IList<Comment> Comments { get; set; }
        [JsonIgnore]
        public IList<Post> Posts { get; set; }
    }
}
