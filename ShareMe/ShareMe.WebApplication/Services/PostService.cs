using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Models.Grid;
using ShareMe.WebApplication.Services.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.Services
{
    public class PostService : Service<PostApiModel, Post>, IPostService
    {
        private readonly PostRepository _postRepository;

        public PostService(UnitOfWork unitOfWork) : base(unitOfWork.PostRepository)
        {
            _postRepository = unitOfWork.PostRepository;
        }

        protected override PostApiModel TranslateToApiModel(Post post)
        {
            return new PostApiModel(post);
        }

        public async Task<GridResult<PostApiModel>> GetGridAsync(PostGridModel postGridModel)
        {
            IQueryable<Post> posts = _postRepository
                .GetByCategoryIdAndTagIds(
                    postGridModel.CategoryId,
                    postGridModel.TagIds);

            return await ApplyGridAsync(postGridModel, posts);
        }
    }
}
