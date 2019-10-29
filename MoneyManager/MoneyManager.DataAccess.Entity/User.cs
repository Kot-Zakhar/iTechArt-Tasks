using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.DataAccess.Entity
{
    public class User : Entity
    {
        public User() : base() { }
        public User(Guid Id) : base(Id) { }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Hash { get; set; }
        
        [Required]
        public string Salt { get; set; }

        public IList<Asset> Assets { get;  set; }
    }
}
