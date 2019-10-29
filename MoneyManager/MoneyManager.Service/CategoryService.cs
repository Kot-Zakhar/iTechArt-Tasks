using MoneyManager.DataAccess.Entity;
using MoneyManager.DataAccess.UnitOfWork;
using MoneyManager.Service.Model;
using System;
using System.Linq;

namespace MoneyManager.Service
{
    class CategoryService
    {
        protected readonly UnitOfWork unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<CategoryInfo> GetParentCategories(CategoryType categoryType)
        {
            return unitOfWork.CategoryRepository.GetCategoriesSorted(categoryType)
                .Where(c => String.IsNullOrEmpty(c.ParentCategory.Name))
                .Select(c => new CategoryInfo(c));
        }




        /// <summary>
        /// "Active category" - category that have transactions in the current month.
        /// </summary>
        public IQueryable<CategoryInfo> GetUserActiveCategoryInfos(Guid userId, CategoryType type)
        {
            return GetUserActiveCategoryInfos(userId).Where(c => c.Type == type);
        }

        /// <summary>
        /// "Active category" - category that have transactions in the current month.
        /// </summary>
        public IQueryable<CategoryInfo> GetUserActiveCategoryInfos(Guid userId)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            DateTime firstDay = new DateTime(year, month, 1);
            DateTime lastDay = firstDay.AddDays(days);

            return unitOfWork.AssetRepository.GetAll()
                .Where(a => a.User.Id == userId)
                .Join(
                    unitOfWork.TransactionRepository.GetAll()
                        .Where(t => t.Date >= firstDay && t.Date <= lastDay),
                    a => a.Id,
                    t => t.Asset.Id,
                    (a, t) => t
                )
                .Join(unitOfWork.CategoryRepository.GetAll(),
                    t => t.Category.Id,
                    c => c.Id,
                    (t, c) => new CategoryInfo(c)
                );
        }

        /// <summary>
        /// Write a query that will return the total amount of all parent categories for the selected type of operation (Income or Expenses).
        /// The result should include only categories that have transactions in the current month. 
        /// In addition, you should order results descending by Transaction.Amount and then ordered them by Category.Name.
        /// </summary>
        public IQueryable<CategoryAmountInfo> GetUserCategoryAmountInfosInCurrentMonth(Guid userId, CategoryType categoryType)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            DateTime firstDay = new DateTime(year, month, 1);
            DateTime lastDay = firstDay.AddDays(days);

            return unitOfWork.AssetRepository.GetUserAssets(userId)
                .Join(
                    unitOfWork.TransactionRepository.GetAll(),
                    a => a.Id,
                    t => t.Asset.Id,
                    (a, t) => t
                )
                .Where(t => t.Date >= firstDay && t.Date <= lastDay && t.Category.Type == categoryType && String.IsNullOrEmpty(t.Category.ParentCategory.Name))
                .GroupBy(t => t.Category)
                .Select(transactionGroup => new CategoryAmountInfo()
                {
                    Category = new CategoryInfo(transactionGroup.Key),
                    Amount = transactionGroup.Aggregate(0.0, (value, transaction) => value + transaction.Amount)
                });
        }
    }
}
