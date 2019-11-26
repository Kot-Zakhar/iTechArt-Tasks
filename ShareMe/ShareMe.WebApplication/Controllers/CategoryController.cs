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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryApiModel>>> GetCategories(int? count)
        {
            if (count != null)
                return await _categoryService.GetTop(count.Value).ToListAsync();
            else
                return await _categoryService.GetAll().ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryApiModel>> GetCategory(Guid id)
        {
            return await _categoryService.GetByIdAsync(id);
        }
    }
}
