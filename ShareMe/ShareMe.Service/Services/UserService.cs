using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareMe.Service
{
    class UserService : Service<User>
    {
        protected readonly UnitOfWork unitOfWork;

        protected IRepository<User> UserRepository { get => this.repository; }

        public UserService(UnitOfWork unitOfWork) : base(unitOfWork.UserRepository)
        {
            this.unitOfWork = unitOfWork;
        }

        public User GetByEmail(string email)
        {
            return UserRepository.GetAll().Single(u => u.Email == email);
        }

        public User GetByUsername(string username)
        {
            return UserRepository.GetAll().Single(u => u.Username == username);
        }
    }
}
