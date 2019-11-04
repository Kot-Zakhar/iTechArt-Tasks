using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using System;
using System.Linq;

namespace ShareMe.Service
{
    public class CategoryService : Service<Category>
    {
        protected readonly UnitOfWork unitOfWork;

        protected IRepository<Category> CategoryRepository { get => this.Repository; }

        public CategoryService(UnitOfWork unitOfWork) : base (unitOfWork.CategoryRepository)
        {
            this.unitOfWork = unitOfWork;
        }

        public Category GetByName(string name)
        {
            return CategoryRepository.GetAll().Single(category => category.Name == name);
        }

        public IQueryable<Category> GetRootCategories()
        {
            return CategoryRepository.GetAll().Where(category => category.Parent == null);
        }

        public override IQueryable<Category> GetAll()
        {
            return unitOfWork.CategoryRepository.GetAll();
        }
    }
}
