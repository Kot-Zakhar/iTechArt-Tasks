using System;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.WebApplication.Models.ApiModels;

namespace ShareMe.WebApplication.Services.Contracts
{
    public interface IService<ApiModelT> where ApiModelT : ApiModel
    {
        IQueryable<ApiModelT> GetAll();
        Task<ApiModelT> GetByIdAsync(Guid id);
    }
}