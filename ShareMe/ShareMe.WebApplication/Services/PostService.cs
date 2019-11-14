using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareMe.WebApplication.Services
{
    public class PostService : Service<Post>
    {
        protected IRepository<Post> PostRepository { get => Repository; }

        public PostService(UnitOfWork unitOfWork) : base(unitOfWork.PostRepository)
        {}


        public override IQueryable<Post> GetAll()
        {
            return PostRepository.GetAll();
        }
    }
}
