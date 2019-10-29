using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManager.DataAccess.Entity
{
    public class Asset : Entity
    {
        public Asset() : base() { }
        public Asset(Guid Id) : base(Id) { }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
