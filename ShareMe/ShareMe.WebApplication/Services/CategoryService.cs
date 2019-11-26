using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<CategoryApiModel> GetByName(string name)
        {
            return TranslateToApiModel(await _categoryRepository.GetByNameAsync(name));
        }

        public IQueryable<CategoryApiModel> GetTop(int count)
        {
            return _categoryRepository.GetAll().Take(count).Select(c => TranslateToApiModel(c));
        }

        public IQueryable<Category> GetAllRootCategories()
        {
            return _categoryRepository.GetAllRootCategories();
        }
    }
}
