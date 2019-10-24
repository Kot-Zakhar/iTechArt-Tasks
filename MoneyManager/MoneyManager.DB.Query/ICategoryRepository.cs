using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Repository
{
    public struct CategoryInfo
    {
        public string Name;
        public string ParentName;
    }
    interface ICategoryRepository : IRepository<Entity.Category>
    {
    }
}
