using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManager.DataAccess.Entity
{
    public enum CategoryType
    {
        Income,
        Expense
    }
    public class Category : Entity
    {
        public Category() : base() { }
        public Category(Guid Id) : base(Id) { }

        [Required]
        public string Name { get;  set; }

        [Required]
        public CategoryType Type { get;  set; }

        [ForeignKey("ParentId")]
        public Category ParentCategory { get; set; }

        public IList<Category> ChildCategories { get; set; }
        public IList<Transaction> Transactions { get; set; }
    }
}
