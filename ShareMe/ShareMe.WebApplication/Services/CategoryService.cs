using System.Collections.Generic;
using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShareMe.WebApplication.Services
{
    public class CategoryService : Service<CategoryApiModel, Category>, ICategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(UnitOfWork unitOfWork) : base(unitOfWork.CategoryRepository)
        {
            _categoryRepository = unitOfWork.CategoryRepository;
        }

        protected override CategoryApiModel TranslateToApiModel(Category category)
        {
            return new CategoryApiModel(category);
        }

        public async Task<CategoryApiModel> GetByNameAsync(string name)
        {
            return TranslateToApiModel(await _categoryRepository.GetByNameAsync(name));
        }

        public async Task<IList<CategoryApiModel>> GetTopAsync(int count)
        {
            return (await _categoryRepository.GetAll().Take(count).ToListAsync()).Select(TranslateToApiModel).ToList();
        }

        public async Task<IList<CategoryApiModel>> GetAllRootCategoriesAsync()
        {
            return (await _categoryRepository.GetAllRootCategories().ToListAsync()).Select(TranslateToApiModel).ToList();
        }
    }
}
