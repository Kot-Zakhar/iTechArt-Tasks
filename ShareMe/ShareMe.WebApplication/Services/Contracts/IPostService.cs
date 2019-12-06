using System.Threading.Tasks;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Models.Grid;

namespace ShareMe.WebApplication.Services.Contracts
{
    public interface IPostService : IService<PostApiModel>
    {
        Task<GridResult<PostApiModel>> GetGridAsync(PostGridModel postGridModel);
    }
}