using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.DataAccessLayer.Entity;

namespace ShareMe.DataAccessLayer.UnitOfWork.Repositories
{
    public interface IRepository<T> where T : Entity.Entity
    {
        Task<T> CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        Task<bool> Delete(T entity);
        Task<bool> DeleteById(Guid? id);
        void Dispose();
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(Guid? id);
        Task Save();
    }
}