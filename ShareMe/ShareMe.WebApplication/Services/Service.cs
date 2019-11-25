using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.Services
{
    public abstract class Service<ApiModelT, DbEntityT> 
        where ApiModelT : ApiModels.ApiModel
        where DbEntityT : DataAccessLayer.Entity.Entity
    {
        private Repository<DbEntityT> _repository;

        protected abstract ApiModelT Translate(DbEntityT entity);

        public Service(Repository<DbEntityT> repository)
        {
            _repository = repository;
        }

        public async Task<ApiModelT> GetById(Guid id)
        {
            return Translate(await _repository.GetByIdAsync(id));
        }

        public IQueryable<ApiModelT> GetAll()
        {
            return _repository.GetAll().Select(e => Translate(e));
        }
    }
}
