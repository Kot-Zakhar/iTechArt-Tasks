using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareMe.WebApplication.Services
{
    public class CommentService : Service<Comment>
    {
        protected IRepository<Comment> CommentRepository { get => Repository; }

        public CommentService(UnitOfWork unitOfWork) : base(unitOfWork.CommentRepository)
        {}

        public IQueryable<Comment> GetCommentsByUserId(Guid userId)
        {
            return CommentRepository.GetAll().Where(comment => comment.Author.Id == userId);
        }

        public IQueryable<Comment> GetCommentsByPostId(Guid postId)
        {
            return CommentRepository.GetAll().Where(comment => comment.Post.Id == postId);
        }

        public int GetAmountOfChildComments(Guid commentId)
        {
            return CommentRepository.GetAll().Where(comment => comment.Id == commentId && comment.ParentComment != null).Count();
        }

        public override IQueryable<Comment> GetAll()
        {
            return CommentRepository.GetAll();
        }
    }
}
