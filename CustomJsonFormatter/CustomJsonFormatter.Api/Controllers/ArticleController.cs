using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomJsonFormatter.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomJsonFormatter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        // GET: api/Article&amount=10
        [HttpGet(Name = "GetArticles")]
        public IEnumerable<Article> Get(int Amount = 10)
        {
            return Enumerable.Range(0, Amount).Select(i => new Article());
        }

        // GET: api/Article/Guid
        [HttpGet("{id}", Name = "GetArticleById")]
        public Article Get(Guid id)
        {
            return new Article(id);
        }
    }
}