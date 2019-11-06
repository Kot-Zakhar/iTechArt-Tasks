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

        // GET: api/Posts?count=10&categoryId=id&tags[]=id1&tags[]=id2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostApiModel>>> GetPosts(
            [FromQuery(Name = "category")] Guid? categoryId,
            [FromQuery(Name = "tags[]")] IList<Guid> tagsIds,
            [FromQuery(Name = "count")] int? count,
            [FromQuery(Name = "rating")] string rating
        ){
            var query = _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.PostTags)
                    .ThenInclude(postTag => postTag.Tag)
                .AsQueryable();
            if (categoryId != null)
                query = query.Where(p => p.Category.Id == categoryId);
            if (tagsIds.Count != 0)
                query = query.Where(p => p.PostTags.Any(pt => tagsIds.Any(tagId => pt.TagId == tagId)));
            if (count != null)
                query = query.Take(count.Value);
            if (rating == "desc")
                query = query.OrderByDescending(p => p.Rating);
            if (rating == "asc")
                query = query.OrderBy(p => p.Rating);
            return await query.Select(p => new PostApiModel(p))
                .ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostApiModel>> GetPost(Guid id)
        {
            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.PostTags)
                    .ThenInclude(postTag => postTag.Tag)
                .Include(p => p.Comments)
                .SingleAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return new PostApiModel(post);
        }
    }
}
