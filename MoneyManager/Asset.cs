using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager
{
    class Asset
    {
        public Guid Id { get; protected set; }
        public string Name { get; set; }
        public Guid UserId { get; protected set; }
    }
}
