using System;
using System.Linq;

namespace MoneyManager.DataAccess.UnitOfWork.Repository
{
    /// <summary>
    /// Task: "For each entity write creating, updating and deleting commands."
    /// Task: "For each entity write queries that will return the entity by identifier."
    /// </summary>
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
