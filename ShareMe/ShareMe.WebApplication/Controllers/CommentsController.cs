﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Services.Contracts;

namespace ShareMe.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            this._commentService = commentService;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IList<CommentApiModel>>> GetComments(Guid postId)
        {
            return (await _commentService.GetCommentsByPostIdAsync(postId)).ToList();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentApiModel>> GetComment(Guid id)
        {
            return await _commentService.GetByIdAsync(id);
        }
    }
}
