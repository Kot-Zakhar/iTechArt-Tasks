using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Entity
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
        public string Name { get;  set; }
        public CategoryType Type { get;  set; }
        public Category ParentCategory { get;  set; }
    }
}
