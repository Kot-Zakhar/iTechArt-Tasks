using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.Services
{
    public class UserService : Service<UserApiModel, User>, IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UnitOfWork unitOfWork) : base(unitOfWork.UserRepository)
        {
            _userRepository = unitOfWork.UserRepository;
        }

        protected override UserApiModel TranslateToApiModel(User user)
        {
            return new UserApiModel(user);
        }

        public async Task<UserApiModel> GetByEmailAsync(string email)
        {
            return TranslateToApiModel(await _userRepository.GetByEmailAsync(email));
        }

        public async Task<UserApiModel> GetByUsernameAsync(string username)
        {
            return TranslateToApiModel(await _userRepository.GetByUsernameAsync(username));
        }
    }
}
