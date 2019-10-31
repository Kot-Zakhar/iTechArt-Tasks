using System;
using System.Linq;

namespace ShareMe.DataAccessLayer.UnitOfWork
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll();
        T GetById(Guid? id);
        T Create(T item);
        void Update(T item);
        bool DeleteById(Guid? id);
        bool Delete(T item);
        void Save();
    }
}
