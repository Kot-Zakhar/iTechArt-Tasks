using System.Linq;
using System.Threading.Tasks;
using ShareMe.WebApplication.Models.ApiModels;

namespace ShareMe.WebApplication.Services.Contracts
{
    public interface ITagService : IService<TagApiModel>
    {
        Task<TagApiModel> GetByName(string name);
        IQueryable<TagApiModel> GetTop(int count);
    }
}