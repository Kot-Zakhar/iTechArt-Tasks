using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareMe.Service
{
    public abstract class Service<T> where T : Entity
    {
        protected IRepository<T> Repository;

        public Service(IRepository<T> repository)
        {
            Repository = repository;
        }

        public abstract IQueryable<T> GetAll();
    }
}
