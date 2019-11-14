using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Context;
using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareMe.DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        protected DbContext context;
        public Repository<User> UserRepository { get; protected set; }
        public Repository<Tag> TagRepository { get; protected set; }
        public Repository<Post> PostRepository { get; protected set; }
        public Repository<Comment> CommentRepository { get; protected set; }
        public Repository<Category> CategoryRepository { get; protected set; }

        public UnitOfWork(ShareMeContext context)
        {
            this.context = context;
            UserRepository = new Repository<User>(context);
            TagRepository = new Repository<Tag>(context);
            PostRepository = new Repository<Post>(context);
            CommentRepository = new Repository<Comment>(context);
            CategoryRepository = new Repository<Category>(context);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void RejectChanges()
        {
            foreach (var entry in context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
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
                    UserRepository.Dispose();
                    TagRepository.Dispose();
                    PostRepository.Dispose();
                    CommentRepository.Dispose();
                    CategoryRepository.Dispose();
                    UserRepository = null;
                    TagRepository = null;
                    PostRepository = null;
                    CommentRepository = null;
                    CategoryRepository = null;
                    context.Dispose();
                    context = null;
                }
            disposed = true;
        }
    }
}
