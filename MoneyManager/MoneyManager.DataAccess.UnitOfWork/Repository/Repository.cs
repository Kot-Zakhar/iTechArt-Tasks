using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MoneyManager.DataAccess.UnitOfWork.Repository
{
    public class Repository<T> : IRepository<T> where T: Entity.Entity
    {
        protected DbContext Context;
        protected DbSet<T> TypeSet;

        public Repository(DbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(typeof(DbContext).FullName);
            TypeSet = context.Set<T>();
        }

        public T Create(T entity)
        {
            return TypeSet.Add(entity).Entity;
        }

        public T GetById(Guid? id)
        {
            return TypeSet.Find(id.GetValueOrDefault(Guid.Empty));
        }

        public IQueryable<T> GetAll()
        {
            return TypeSet;
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public bool Delete(T entity)
        {
            return DeleteById(entity?.Id);
        }

        public bool DeleteById(Guid? id)
        {
            T entity = GetById(id.GetValueOrDefault(Guid.Empty));
            if (entity != null)
                TypeSet.Remove(entity);
            return entity != null;
        }

        public void Save()
        {
            Context.SaveChanges();
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
                    Context.Dispose();
                    TypeSet = null;
                    Context = null;
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
