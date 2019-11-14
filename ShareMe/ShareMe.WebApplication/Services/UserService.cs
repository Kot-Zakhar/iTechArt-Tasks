using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repository;
using ShareMe.WebApplication.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.Services
{
    public class UserService : Service<User>
    {
        protected IRepository<User> UserRepository { get => Repository; }

        public UserService(UnitOfWork unitOfWork) : base(unitOfWork.UserRepository)
        {}

        public User GetByEmail(string email)
        {
            return UserRepository.GetAll().Single(u => u.Email == email);
        }

        public User GetByUsername(string username)
        {
            return UserRepository.GetAll().Single(u => u.Username == username);
        }

        public override IQueryable<User> GetAll()
        {
            return UserRepository.GetAll();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return UserRepository.GetById(id);
        }
    }
}
