using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager
{
    public class Transaction
    {
        public Transaction()
        {
            Id = Guid.NewGuid();
        }
        public Transaction(Guid Id)
        {
            this.Id = Id;
        }
        public Guid Id { get; protected set; }
        public Category Category { get;  set; }
        public double Amount { get; set; }
        public DateTime Date { get;  set; }
        public Asset Asset { get;  set; }
        public string Comment { get; set; }

        
    }
}
