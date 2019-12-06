using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.WebApplication.Models.ApiModels;

namespace ShareMe.WebApplication.Services.Contracts
{
    public interface IService<ApiModelT> where ApiModelT : ApiModel
    {
        Task<IList<ApiModelT>> GetAllAsync();
        Task<ApiModelT> GetByIdAsync(Guid id);
    }
}