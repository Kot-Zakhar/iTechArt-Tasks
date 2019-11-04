using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareMe.Service
{
    public class UserService : Service<User>
    {
        protected readonly UnitOfWork unitOfWork;

        protected IRepository<User> UserRepository { get => this.Repository; }

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

        public override IQueryable<User> GetAll()
        {
            return unitOfWork.UserRepository.GetAll();
        }
    }
}
