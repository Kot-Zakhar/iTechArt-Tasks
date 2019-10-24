using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Entity
{
    public class User : Entity
    {
        public User() : base() { }
        public User(Guid Id) : base(Id) { }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public ICollection<Asset> Assets { get;  set; }
    }
}
