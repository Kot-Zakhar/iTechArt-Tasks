using System.Threading.Tasks;
using ShareMe.WebApplication.Models.ApiModels;

namespace ShareMe.WebApplication.Services.Contracts
{
    public interface IUserService : IService<UserApiModel>
    {
        Task<UserApiModel> GetByEmailAsync(string email);
        Task<UserApiModel> GetByUsernameAsync(string username);
    }
}