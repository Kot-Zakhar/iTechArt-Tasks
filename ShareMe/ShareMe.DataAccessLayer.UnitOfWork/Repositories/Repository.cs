using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.DataAccessLayer.UnitOfWork.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity.Entity
    {
        protected DbContext context;
        protected DbSet<T> typeSet;

        public Repository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(typeof(DbContext).FullName);
            typeSet = context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var result = await typeSet.AddAsync(entity);
            return result.Entity;
        }

        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            await typeSet.AddRangeAsync(entities);
        }

        public async Task<T> GetByIdAsync(Guid? id)
        {
            return await typeSet.FindAsync(id.GetValueOrDefault(Guid.Empty));
        }

        public IQueryable<T> GetAll()
        {
            return typeSet;
        }

        public async Task<bool> Delete(T entity)
        {
            return await DeleteById(entity?.Id);
        }

        public async Task<bool> DeleteById(Guid? id)
        {
            T entity = await GetByIdAsync(id.GetValueOrDefault(Guid.Empty));
            if (entity != null)
                typeSet.Remove(entity);
            return entity != null;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }


        private bool disposed = false;

        ~Repository()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                {
                    context.Dispose();
                    typeSet = null;
                    context = null;
                }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
