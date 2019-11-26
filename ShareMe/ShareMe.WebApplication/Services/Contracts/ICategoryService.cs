using System.Linq;
using System.Threading.Tasks;
using ShareMe.DataAccessLayer.Entity;
using ShareMe.WebApplication.Models.ApiModels;

namespace ShareMe.WebApplication.Services.Contracts
{
    public interface ICategoryService : IService<CategoryApiModel>
    {
        IQueryable<Category> GetAllRootCategories();
        Task<CategoryApiModel> GetByName(string name);
        IQueryable<CategoryApiModel> GetTop(int count);
    }
}