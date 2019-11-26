using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Models.Grid;
using ShareMe.WebApplication.Services.Contracts;

namespace ShareMe.WebApplication.Services
{
    public abstract class Service<ApiModelT, DbEntityT> : IService<ApiModelT> where ApiModelT : ApiModel
        where DbEntityT : DataAccessLayer.Entity.Entity
    {
        private readonly Repository<DbEntityT> _repository;

        protected abstract ApiModelT TranslateToApiModel(DbEntityT entity);

        public Service(Repository<DbEntityT> repository)
        {
            _repository = repository;
        }

        public async Task<ApiModelT> GetByIdAsync(Guid id)
        {
            return TranslateToApiModel(await _repository.GetByIdAsync(id));
        }

        public IQueryable<ApiModelT> GetAll()
        {
            return _repository.GetAll().Select(e => TranslateToApiModel(e));
        }

        protected async Task<GridResult<ApiModelT>> ApplyGridAsync(GridModel<ApiModelT> postGridModel, IQueryable<ApiModelT> posts)
        {

            // todo: check what is returned typ typeof(ApiModelT)
            if (postGridModel.IsFiltering)
            {
                PropertyInfo filterProperty = typeof(ApiModelT).GetProperty(postGridModel.FilterField);
                if (filterProperty != null)
                    posts = posts.Where(post => postGridModel.FilterValues.Any(value => filterProperty.GetValue(post).ToString() == value));
            }


            if (postGridModel.IsSorting)
            {
                PropertyInfo sortProperty = typeof(ApiModelT).GetProperty(postGridModel.SortField);
                if (sortProperty != null)
                {
                    Expression<Func<ApiModelT, object>> orderExpression = p => sortProperty.GetValue(p);
                    posts = postGridModel.IsSortingASC ? posts.OrderBy(orderExpression) : posts.OrderByDescending(orderExpression);
                }
            }

            IQueryable<ApiModelT> page = posts
                .Skip(postGridModel.PageIndex * postGridModel.PageSize)
                .Take(postGridModel.PageSize);

            var result = new GridResult<ApiModelT>()
            {
                Values = await page.ToListAsync(),
                PageIndex = postGridModel.PageIndex,
                PageSize = postGridModel.PageSize,
                Next = posts.Skip((postGridModel.PageIndex + 1) * postGridModel.PageSize).Take(postGridModel.PageSize).Any(),
                Previous = postGridModel.PageIndex > 0
            };
            return result;
        }
    }
}
