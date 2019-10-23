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
    public class Category
    {
        public Category()
        {
            Id = Guid.NewGuid();
        }
        public Category(Guid Id)
        {
            this.Id = Id;
        }
        public Guid Id { get; protected set; }
        public string Name { get;  set; }
        public CategoryType Type { get;  set; }
        public Category ParentCategory { get;  set; }
    }
}
