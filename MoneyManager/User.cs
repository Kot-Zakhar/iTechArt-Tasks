using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager
{
    class User
    {
        public Guid id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Hash { get; protected set; }
        public string Salt { get; protected set; }

    }
}
