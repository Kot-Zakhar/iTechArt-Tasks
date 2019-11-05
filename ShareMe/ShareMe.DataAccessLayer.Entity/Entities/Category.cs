using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace ShareMe.DataAccessLayer.Entity
{
    public class Category : Entity
    {
        [JsonIgnore]
        public Category ParentCategory { get; set; }

        [NotMapped]
        public Guid ParentCategoryId { get => ParentCategory.Id; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public IList<Post> Posts { get; set; } = new List<Post>();
        [JsonIgnore]
        public IList<Category> ChildCategories { get; set; } = new List<Category>();
    }
}
