using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repository;
using System;
using System.Linq;

namespace ShareMe.WebApplication.Services
{
    public class CategoryService : Service<Category>
    {
        protected IRepository<Category> CategoryRepository { get => Repository; }

        public CategoryService(UnitOfWork unitOfWork) : base(unitOfWork.CategoryRepository)
        {}

        public Category GetByName(string name)
        {
            return CategoryRepository.GetAll().Single(category => category.Name == name);
        }

        public IQueryable<Category> GetRootCategories()
        {
            return CategoryRepository.GetAll().Where(category => category.ParentCategory == null);
        }

        public override IQueryable<Category> GetAll()
        {
            return CategoryRepository.GetAll();
        }
    }
}
