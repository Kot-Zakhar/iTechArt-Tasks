using MoneyManager.DataAccess.Entity;
using MoneyManager.DataAccess.UnitOfWork;
using MoneyManager.Service.Model;
using System;
using System.Linq;

namespace MoneyManager.Service
{
    public class UserService
    {
        protected readonly UnitOfWork UnitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }


        /// <summary>
        /// Task: "Write a query to return the user list sorted by the user’s name."
        /// </summary>
        public IQueryable<UserInfo> GetInfos()
        {
            return UnitOfWork.UserRepository.GetUsersSorted()
                    .Select(user => new UserInfo(user))
                    .OrderBy(userInfo => userInfo.Name);
        }

        /// <summary>
        /// Task: "Write a query that will return the current balance for the user."
        /// </summary>
        public UserBalance GetUserBalance(Guid userId)
        {
            return new UserBalance()
            {
                UserInfo = new UserInfo(UnitOfWork.UserRepository.GetById(userId)),
                Balance = UnitOfWork.AssetRepository.GetUserAssets(userId)
                    .Join(
                        UnitOfWork.TransactionRepository.GetAll(),
                        asset => asset.Id,
                        transaction => transaction.Asset.Id,
                        (asset, transaction) => transaction
                    )
                    .Sum(transaction => (transaction.Category.Type == CategoryType.Income ? 1 : -1) * transaction.Amount)
            };
        }

        /// <summary>
        /// Write a query that will return the total value of income and expenses for the selected period (parameters userId, startDate, endDate)
        /// Ordered by date and grouped by month.
        /// </summary>
        public IQueryable<UserMonthIncomeAndExpensesInfo> GetUserMonthIncomeAndExpensesInfos(Guid userId, DateTime startDate, DateTime endDate)
        {
            return UnitOfWork.AssetRepository.GetUserAssets(userId)
                .Join(
                    UnitOfWork.TransactionRepository.GetAll().Where(transaction => transaction.Date >= startDate && transaction.Date <= endDate),
                    asset => asset.Id,
                    transaction => transaction.Asset.Id,
                    (asset, transaction) => transaction
                )
                .GroupBy(transaction => new DateTime(transaction.Date.Year, transaction.Date.Month, 1))
                .Select(transactionGroup => new UserMonthIncomeAndExpensesInfo()
                {
                    Month = transactionGroup.Key.Month,
                    Year = transactionGroup.Key.Year,
                    UserInfo = new UserInfo(UnitOfWork.UserRepository.GetById(userId)),
                    Expenses = transactionGroup.Where(transaction => transaction.Category.Type == CategoryType.Expense).Sum(transaction => transaction.Amount),
                    Income = transactionGroup.Where(transaction => transaction.Category.Type == CategoryType.Income).Sum(transaction => transaction.Amount)
                });
        }
    }
}
