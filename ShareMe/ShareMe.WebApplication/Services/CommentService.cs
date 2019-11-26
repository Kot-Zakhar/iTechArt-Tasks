using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Services.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.Services
{
    public class CommentService : Service<CommentApiModel, Comment>, ICommentService
    {
        private readonly CommentRepository _commentRepository;

        public CommentService(UnitOfWork unitOfWork) : base(unitOfWork.CommentRepository)
        {
            _commentRepository = unitOfWork.CommentRepository;
        }

        protected override CommentApiModel TranslateToApiModel(Comment comment)
        {
            return new CommentApiModel(comment);
        }

        public IQueryable<CommentApiModel> GetCommentsByUserId(Guid userId)
        {
            return _commentRepository.GetByUserId(userId).Select(c => TranslateToApiModel(c));
        }

        public IQueryable<CommentApiModel> GetCommentsByPostId(Guid postId)
        {
            return _commentRepository.GetByPostId(postId).Select(c => TranslateToApiModel(c));
        }

        public async Task<int> GetAmountOfChildCommentsAsync(Guid parentCommentId)
        {
            return await _commentRepository.GetChildComments(parentCommentId).CountAsync();
        }

        public IQueryable<CommentApiModel> GetChildComments(Guid parentCommentId)
        {
            return _commentRepository.GetChildComments(parentCommentId).Select(c => TranslateToApiModel(c));
        }
    }
}
