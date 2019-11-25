using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using ShareMe.WebApplication.ApiModels;
using System.Linq;

namespace ShareMe.WebApplication.Services
{
    public class PostService : Service<PostApiModel, Post>
    {
        private PostRepository _postRepository;

        public PostService(UnitOfWork unitOfWork) : base(unitOfWork.PostRepository)
        {
            _postRepository = unitOfWork.PostRepository;
        }

        protected override PostApiModel Translate(Post entity)
        {
            return new PostApiModel(entity);
        }
    }
}
