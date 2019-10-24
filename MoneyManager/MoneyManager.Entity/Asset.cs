using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Entity
{
    public class Asset : Entity
    {
        public Asset() : base() { }
        public Asset(Guid Id) : base(Id) { }
        public string Name { get; set; }
        public User User { get; set; }
    }
}
