using System;
using System.Linq;
using System.Data.Entity;
using MoneyManager.Repository;

namespace MoneyManager.MSSQLLocalDBRepository
{
    public class Repository<T> : IRepository<T> where T: Entity.Entity
    {
        protected DbContext context;
        protected DbSet<T> typeSet;

        public Repository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(typeof(DbContext).FullName);
            typeSet = context.Set<T>();
        }

        public T Create(T entity)
        {
            return typeSet.Add(entity);
        }

        public T GetById(Guid? id)
        {
            return typeSet.Find(id.GetValueOrDefault(Guid.Empty));
        }

        public IQueryable<T> GetAll()
        {
            return typeSet;
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public bool Delete(T entity)
        {
            return DeleteById(entity?.Id);
        }

        public bool DeleteById(Guid? id)
        {
            T entity = GetById(id.GetValueOrDefault(Guid.Empty));
            if (entity != null)
                typeSet.Remove(entity);
            return entity != null;
        }

        public void Save()
        {
            context.SaveChanges();
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
