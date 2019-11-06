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
    public class PostsController : ControllerBase
    {
        private readonly ShareMeContext _context;

        public PostsController(ShareMeContext context)
        {
            _context = context;
        }

        // GET: api/Posts?count=10&categoryId=id&tagsIds=[id1,id2]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostApiModel>>> GetPosts(Guid? categoryId, IList<Guid> tagsIds, int? count = 10)
        {
            return await _context.Posts
                .Where(p => categoryId == null || p.Category.Id == categoryId)
                .Where(p => tagsIds == null || tagsIds.Count == 0 || p.PostTags.Any(pt => tagsIds.Any(tagId => pt.TagId == tagId)))
                .TakeWhile((p, index) => count == null || index < count)
                .Select(p => new PostApiModel(p))
                .ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostApiModel>> GetPost(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);
            //var post = PostFaker.Generate();

            if (post == null)
            {
                return NotFound();
            }

            return new PostApiModel(post);
        }
    }
}
