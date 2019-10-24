using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        T Create(T item);
        void Update(T item);
        bool DeleteById(Guid id);
        bool Delete(T item);
        void Save();
    }
}
