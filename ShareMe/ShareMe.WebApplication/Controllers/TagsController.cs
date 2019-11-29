using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Services;

namespace ShareMe.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagsController(TagService tagService)
        {
            _tagService = tagService;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IList<TagApiModel>>> GetTags(int? count)
        {
            if (count != null)
                return (await _tagService.GetTopAsync(count.Value)).ToList();
            else
                return (await _tagService.GetAllAsync()).ToList();
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagApiModel>> GetTag(Guid id)
        {
            return await _tagService.GetByIdAsync(id);
        }
    }
}
