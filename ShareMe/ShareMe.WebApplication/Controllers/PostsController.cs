using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Models.Grid;
using ShareMe.WebApplication.Services;

namespace ShareMe.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;

        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<GridResult<PostApiModel>>> GetPostsAsync(
            [FromQuery] PostGridModel postGridModel
        ){
            return await _postService.GetGridAsync(postGridModel);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostApiModel>> GetPostAsync(Guid id)
        {
            return await _postService.GetByIdAsync(id);
        }
    }
}
