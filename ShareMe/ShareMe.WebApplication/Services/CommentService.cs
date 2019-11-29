using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Services.Contracts;
using System;
using System.Collections.Generic;
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

        public async Task<IList<CommentApiModel>> GetCommentsByUserIdAsync(Guid userId)
        {
            return await _commentRepository.GetByUserId(userId).Select(c => TranslateToApiModel(c)).ToListAsync();
        }

        public async Task<IList<CommentApiModel>> GetCommentsByPostIdAsync(Guid postId)
        {
            return await _commentRepository.GetByPostId(postId).Select(c => TranslateToApiModel(c)).ToListAsync();
        }

        public async Task<int> GetAmountOfChildCommentsAsync(Guid parentCommentId)
        {
            return await _commentRepository.GetChildComments(parentCommentId).CountAsync();
        }

        public async Task<IList<CommentApiModel>> GetChildCommentsAsync(Guid parentCommentId)
        {
            return await _commentRepository.GetChildComments(parentCommentId).Select(c => TranslateToApiModel(c)).ToListAsync();
        }
    }
}
