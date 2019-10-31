using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.Service
{
    public abstract class Service<T> where T : Entity
    {
        protected IRepository<T> repository;

        public Service(IRepository<T> repository)
        {
            this.repository = repository;
        }

        // crud
    }
}
