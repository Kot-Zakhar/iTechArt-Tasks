using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager
{
    enum CategoryType
    {
        All
    }
    class Category
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public CategoryType Type { get; protected set; }
        public Guid ParentId { get; protected set; }
    }
}
