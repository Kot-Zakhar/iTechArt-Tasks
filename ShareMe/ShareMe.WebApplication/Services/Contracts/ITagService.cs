using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.WebApplication.Models.ApiModels;

namespace ShareMe.WebApplication.Services.Contracts
{
    public interface ITagService : IService<TagApiModel>
    {
        Task<TagApiModel> GetByNameAsync(string name);
        Task<IList<TagApiModel>> GetTopAsync(int count);
    }
}