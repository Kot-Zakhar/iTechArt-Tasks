using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShareMe.WebApplication.Models.ApiModels;

namespace ShareMe.WebApplication.Services.Contracts
{
    public interface ICommentService : IService<CommentApiModel>
    {
        Task<int> GetAmountOfChildCommentsAsync(Guid parentCommentId);
        Task<IList<CommentApiModel>> GetChildCommentsAsync(Guid parentCommentId);
        Task<IList<CommentApiModel>> GetCommentsByPostIdAsync(Guid postId);
        Task<IList<CommentApiModel>> GetCommentsByUserIdAsync(Guid userId);
    }
}