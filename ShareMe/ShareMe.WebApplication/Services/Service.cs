using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    public abstract class Service<ApiModelT, DbEntityT> : IService<ApiModelT>
        where ApiModelT : ApiModel
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

        public async Task<IList<ApiModelT>> GetAllAsync()
        {
            return (await _repository.GetAll().ToListAsync()).Select(e => TranslateToApiModel(e)).ToList();
        }

        protected async Task<GridResult<ApiModelT>> ApplyGridAsync(GridModel<ApiModelT> entityGridModel, IQueryable<DbEntityT> entities)
        {
            var type = typeof(DbEntityT);

            if (entityGridModel.IsFiltering)
            {
                // entities = entities.Where(post => entityGridModel.FilterValues.Contains(filterProperty.GetValue(post).ToString()));
             
                PropertyInfo filterProperty = type.GetProperty(entityGridModel.FilterField);
                var parameter = Expression.Parameter(type, filterProperty.Name);
                var propertyAccess = Expression.MakeMemberAccess(parameter, filterProperty);

                MethodInfo toString = filterProperty.PropertyType.GetMethod("ToString", new Type[] {});
                var toStringExp = Expression.Call(propertyAccess, toString);

                MethodInfo containsMethod = typeof(List<string>).GetMethod("Contains", new Type[] {typeof(string)});
                var values = Expression.Constant(entityGridModel.FilterValues);
                var containsExp = Expression.Call(values, containsMethod, toStringExp);

                var whereExp = Expression.Lambda<Func<DbEntityT, bool>>(containsExp, parameter);

                entities = entities.Where(whereExp);
            }


            if (entityGridModel.IsSorting)
            {

                //Expression<Func<ApiModelT, object>> orderExpression = p => sortProperty.GetValue(p);
                //posts = postGridModel.IsSortingASC ? posts.OrderBy(orderExpression) : posts.OrderByDescending(orderExpression);

                PropertyInfo sortProperty = type.GetProperty(entityGridModel.SortField);
                var parameter = Expression.Parameter(type, sortProperty.Name);
                var propertyAccess = Expression.MakeMemberAccess(parameter, sortProperty);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                var typeArguments = new Type[] {type, sortProperty.PropertyType};
                var methodName = entityGridModel.IsSortingASC ? "OrderBy" : "OrderByDescending";
                var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, entities.Expression,
                    Expression.Quote(orderByExp));
                entities = entities.Provider.CreateQuery<DbEntityT>(resultExp);
            }

            IQueryable<DbEntityT> page = entities
                .Skip(entityGridModel.PageIndex * entityGridModel.PageSize)
                .Take(entityGridModel.PageSize);

            var result = new GridResult<ApiModelT>()
            {
                Values = (await page.ToListAsync()).Select(e => TranslateToApiModel(e)).ToList(),
                PageIndex = entityGridModel.PageIndex,
                PageSize = entityGridModel.PageSize,
                Next = entities.Skip((entityGridModel.PageIndex + 1) * entityGridModel.PageSize).Take(entityGridModel.PageSize).Any(),
                Previous = entityGridModel.PageIndex > 0
            };
            return result;
        }
    }
}
