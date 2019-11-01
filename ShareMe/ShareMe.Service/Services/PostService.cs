using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareMe.Service
{
    class PostService : Service<Post>
    {
        protected readonly UnitOfWork unitOfWork;

        protected IRepository<Post> PostRepository { get => this.repository; }

        public PostService(UnitOfWork unitOfWork) : base(unitOfWork.PostRepository)
        {
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Post> GetPostsByAuthorId(Guid authorId)
        {
            return PostRepository.GetAll().Where(post => post.Author.Id == authorId);
        }

        public Post GetByUri(string uri)
        {
            return PostRepository.GetAll().Single(post => post.URI == uri);
        }

        public IQueryable<Post> GetPostsByTagId(Guid tagId)
        {
            return PostRepository.GetAll().Where(post => post.PostTags.Any(postTag => postTag.Tag.Id == tagId));
        }

        public IQueryable<Post> GetPostsByCategoryId(Guid categoryId)
        {
            return PostRepository.GetAll().Where(post => post.Category.Id == categoryId);
        }

        public IQueryable<Post> GetTopRatedPosts(int amount)
        {
            return PostRepository.GetAll().OrderByDescending(post => post.Rating).Take(amount);
        }
    }
}
