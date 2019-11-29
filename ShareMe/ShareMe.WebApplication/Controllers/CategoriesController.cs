using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Services;

namespace ShareMe.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IList<CategoryApiModel>>> GetCategories(int? count)
        {
            if (count != null)
                return (await _categoryService.GetTopAsync(count.Value)).ToList();
            else
                return (await _categoryService.GetAllAsync()).ToList();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryApiModel>> GetCategory(Guid id)
        {
            return await _categoryService.GetByIdAsync(id);
        }
    }
}
