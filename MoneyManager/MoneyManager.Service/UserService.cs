using MoneyManager.DataAccess.Entity;
using MoneyManager.DataAccess.UnitOfWork;
using MoneyManager.Service.Model;
using System;
using System.Linq;

namespace MoneyManager.Service
{
    public class UserService
    {
        protected readonly UnitOfWork unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Task: "Write a query to return the user list sorted by the user’s name."
        /// </summary>
        public IQueryable<UserInfo> GetInfos()
        {
            return unitOfWork.UserRepository.GetUsersSorted()
                    .Select(user => new UserInfo(user))
                    .OrderBy(userInfo => userInfo.Name);
        }

        /// <summary>
        /// Task: "Write a query that will return the current balance for the user."
        /// </summary>
        public UserBalance GetUserBalance(Guid userId)
        {
            return unitOfWork.AssetRepository.GetUserAssets(userId)
                .Join(
                    unitOfWork.TransactionRepository.GetAll(),
                    a => a.Id,
                    t => t.Asset.Id,
                    (a, t) => t
                )
                .Aggregate(
                    0.0,
                    (value, t) => value + (t.Category.Type == CategoryType.Income? 1 : -1) * t.Amount,
                    value => new UserBalance() { Balance = value, UserInfo = new UserInfo(unitOfWork.UserRepository.GetById(userId)) }
                );
        }

        /// <summary>
        /// Write a query that will return the total value of income and expenses for the selected period (parameters userId, startDate, endDate)
        /// Ordered by date and grouped by month.
        /// </summary>
        public IQueryable<UserMonthIncomeAndExpensesInfo> GetUserMonthIncomeAndExpensesInfos(Guid userId, DateTime startDate, DateTime endDate)
        {
            return unitOfWork.AssetRepository.GetUserAssets(userId)
                .Join(
                    unitOfWork.TransactionRepository.GetAll().Where(t => t.Date >= startDate && t.Date <= endDate),
                    a => a.Id,
                    t => t.Asset.Id,
                    (a, t) => t
                )
                .GroupBy(t => new DateTime(t.Date.Year, t.Date.Month, 1))
                .Select(transactionGroup => new UserMonthIncomeAndExpensesInfo()
                {
                    Month = transactionGroup.Key.Month,
                    Year = transactionGroup.Key.Year,
                    UserInfo = new UserInfo(unitOfWork.UserRepository.GetById(userId)),
                    Expenses = transactionGroup.Where(t => t.Category.Type == CategoryType.Expense)
                                .Aggregate(0.0, (value, transaction) => value + transaction.Amount),
                    Income = transactionGroup.Where(t => t.Category.Type == CategoryType.Income)
                                .Aggregate(0.0, (value, transaction) => value + transaction.Amount),

                });
        }
    }
}
