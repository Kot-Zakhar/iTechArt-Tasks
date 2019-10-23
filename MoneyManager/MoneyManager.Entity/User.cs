using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Entity
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
        }
        public User(Guid Id)
        {
            this.Id = Id;
        }
        public Guid Id { get; protected set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public ICollection<Asset> Assets { get;  set; }
    }
}
