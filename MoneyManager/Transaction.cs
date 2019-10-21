using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager
{
    class Transaction
    {
        public Guid Id { get; protected set; }
        public Guid CategoryId { get; protected set; }
        public uint Amount { get; set; }
        public DateTime Date { get; protected set; }
        public Guid AssetId { get; protected set; }
        public string Comment { get; set; }

        
    }
}
