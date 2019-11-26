using System;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.WebApplication.Models.ApiModels;

namespace ShareMe.WebApplication.Services.Contracts
{
    public interface ICommentService : IService<CommentApiModel>
    {
        Task<int> GetAmountOfChildCommentsAsync(Guid parentCommentId);
        IQueryable<CommentApiModel> GetChildComments(Guid parentCommentId);
        IQueryable<CommentApiModel> GetCommentsByPostId(Guid postId);
        IQueryable<CommentApiModel> GetCommentsByUserId(Guid userId);
    }
}