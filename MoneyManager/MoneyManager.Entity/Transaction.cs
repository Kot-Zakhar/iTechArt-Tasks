using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Entity
{
    public class Transaction : Entity
    {
        public Transaction() : base() { }
        public Transaction(Guid Id) : base(Id) { }
        public Category Category { get;  set; }
        public double Amount { get; set; }
        public DateTime Date { get;  set; }
        public Asset Asset { get;  set; }
        public string Comment { get; set; }

        
    }
}
