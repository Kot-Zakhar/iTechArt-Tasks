using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.DataAccessLayer.Entity;
using ShareMe.WebApplication.Models.ApiModels;

namespace ShareMe.WebApplication.Services.Contracts
{
    public interface ICategoryService : IService<CategoryApiModel>
    {
        Task<IList<CategoryApiModel>> GetAllRootCategoriesAsync();
        Task<CategoryApiModel> GetByNameAsync(string name);
        Task<IList<CategoryApiModel>> GetTopAsync(int count);

    }
}