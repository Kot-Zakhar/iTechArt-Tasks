using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Context;
using ShareMe.DataAccessLayer.Entity;
using ShareMe.WebApplication.ApiModels;

namespace ShareMe.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ShareMeContext _context;

        public CommentsController(ShareMeContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentApiModel>>> GetComments(Guid? postId)
        {
            if (postId == null)
                return NotFound();
            return await _context.Comments
                .Where(c => c.Post.Id == postId)
                .Select(c => new CommentApiModel(c))
                .ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentApiModel>> GetComment(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return new CommentApiModel(comment);
        }
    }
}
