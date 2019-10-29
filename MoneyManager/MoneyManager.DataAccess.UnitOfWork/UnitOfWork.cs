using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccess.Context;
using MoneyManager.DataAccess.UnitOfWork.Repository;

namespace MoneyManager.DataAccess.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        protected DbContext dbContext;
        public UserRepository UserRepository { get; protected set; }

        public CategoryRepository CategoryRepository { get; protected set; }

        public AssetRepository AssetRepository { get; protected set; }

        public TransactionRepository TransactionRepository { get; protected set; }

        public UnitOfWork()
        {
            dbContext = new MoneyManagerContext();
            UserRepository = new UserRepository(dbContext);
            CategoryRepository = new CategoryRepository(dbContext);
            AssetRepository = new AssetRepository(dbContext);
            TransactionRepository = new TransactionRepository(dbContext);
        }


        /// <summary>
        /// Commits all changes
        /// </summary>
        public void Commit()
        {
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Discards all changes that has not been commited
        /// </summary>
        public void RejectChanges()
        {
            foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        bool disposed = false;
        ~UnitOfWork()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                {
                    TransactionRepository.Dispose();
                    TransactionRepository = null;
                    UserRepository.Dispose();
                    UserRepository = null;
                    AssetRepository.Dispose();
                    AssetRepository = null;
                    CategoryRepository.Dispose();
                    CategoryRepository = null;
                    dbContext.Dispose();
                    dbContext = null;
                }
            disposed = true;
        }
    }
}
