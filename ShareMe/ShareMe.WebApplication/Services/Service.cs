using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.Services
{
    public abstract class Service<T> where T : Entity
    {
        protected IRepository<T> Repository;

        public Service(IRepository<T> repository)
        {
            Repository = repository;
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }
    }
}
