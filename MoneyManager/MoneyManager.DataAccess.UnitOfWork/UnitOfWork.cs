using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoneyManager.DataAccess.Context;
using MoneyManager.DataAccess.UnitOfWork.Repository;

namespace MoneyManager.DataAccess.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        protected DbContext DbContext;
        public UserRepository UserRepository { get; protected set; }

        public CategoryRepository CategoryRepository { get; protected set; }

        public AssetRepository AssetRepository { get; protected set; }

        public TransactionRepository TransactionRepository { get; protected set; }

        public UnitOfWork(DbContextOptions options)
        {
            DbContext = new MoneyManagerContext(options);
            UserRepository = new UserRepository(DbContext);
            CategoryRepository = new CategoryRepository(DbContext);
            AssetRepository = new AssetRepository(DbContext);
            TransactionRepository = new TransactionRepository(DbContext);
        }


        /// <summary>
        /// Commits all changes
        /// </summary>
        public void Commit()
        {
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Discards all changes that has not been commited
        /// </summary>
        public void RejectChanges()
        {
            foreach (var entry in DbContext.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
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
                    DbContext.Dispose();
                    DbContext = null;
                }
            disposed = true;
        }
    }
}
