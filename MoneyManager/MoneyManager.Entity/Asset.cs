using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Entity
{
    public class Asset
    {
        public Asset()
        {
            Id = Guid.NewGuid();
        }
        public Asset(Guid Id)
        {
            this.Id = Id;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
    }
}
