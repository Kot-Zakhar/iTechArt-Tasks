using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using ShareMe.WebApplication.ApiModels;
using System;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.Services
{
    public class UserService : Service<UserApiModel, User>
    {
        private UserRepository _userRepository;

        public UserService(UnitOfWork unitOfWork) : base(unitOfWork.UserRepository)
        {
            _userRepository = unitOfWork.UserRepository;
        }

        protected override UserApiModel Translate(User entity)
        {
            return new UserApiModel(entity);
        }

        public async Task<UserApiModel> GetByEmailAsync(string email)
        {
            return Translate(await _userRepository.GetByEmailAsync(email));
        }

        public async Task<UserApiModel> GetByUsernameAsync(string username)
        {
            return Translate(await _userRepository.GetByUsernameAsync(username));
        }

        public async Task<UserApiModel> GetByIdAsync(Guid id)
        {
            return Translate(await _userRepository.GetByIdAsync(id));
        }

    }
}
