using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.DataAccessLayer.UnitOfWork.Repositories
{
    public class CommentRepository : Repository<Comment>
    {
        private DbSet<Comment> CommentTypeSet => typeSet;
        public CommentRepository(DbContext context) : base(context) { }

        public IQueryable<Comment> GetByUserId(Guid userId)
        {
            return CommentTypeSet.Where(c => c.Author.Id == userId);
        }

        public IQueryable<Comment> GetByPostId(Guid postId)
        {
            return CommentTypeSet.Where(c => c.Post.Id == postId);
        }

        public IQueryable<Comment> GetChildComments(Guid parentCommentId)
        {
            return CommentTypeSet.Where(c => c.ParentComment.Id == parentCommentId);
        }
    }
}
