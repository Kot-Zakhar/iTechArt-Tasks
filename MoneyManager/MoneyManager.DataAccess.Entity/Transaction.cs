using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManager.DataAccess.Entity
{
    public class Transaction : Entity
    {
        public Transaction() : base() { }
        public Transaction(Guid Id) : base(Id) { }

        [Required]
        [ForeignKey("CategoryId")]
        public Category Category { get;  set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime Date { get;  set; }

        [Required]
        [ForeignKey("AssetId")]
        public Asset Asset { get;  set; }

        [StringLength(1024)]
        public string Comment { get; set; }

        
    }
}
