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
    public class TagsController : ControllerBase
    {
        private readonly ShareMeContext _context;

        public TagsController(ShareMeContext context)
        {
            _context = context;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagApiModel>>> GetTags(int? count = 10)
        {
            return await _context.Tags
                .TakeWhile((t, index) => count == null || index < count)
                .Select(t => new TagApiModel(t))
                .ToListAsync();
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagApiModel>> GetTag(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return new TagApiModel(tag);
        }
    }
}
