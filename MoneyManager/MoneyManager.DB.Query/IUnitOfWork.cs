using System;
using System.Collections.Generic;
using MoneyManager.Entity;

namespace MoneyManager.Repository
{
    public struct UserIncomeAndExpensesInfo
    {
        public UserInfo UserInfo;
        public double Income;
        public double Expenses;
        public int Month;
        public int Year;
    }
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IAssetRepository AssetRepository { get; }
        ITransactionRepository TransactionRepository { get; }

        /// <summary>
        /// Commits all changes
        /// </summary>
        void Commit();

        /// <summary>
        /// Discards all changes that has not been commited
        /// </summary>
        void RejectChanges();


        /// <summary>
        /// Ordered by date and grouped by month.
        /// </summary>
        public IEnumerable<UserIncomeAndExpensesInfo> GetUserIncomeAndExpensesInfos(Guid userId, DateTime from, DateTime to);

        /// <summary>
        /// "Active category" - category that have transactions in the current month.
        /// </summary>
        public IEnumerable<CategoryInfo> GetActiveCategoryInfosByType(CategoryType type);

        /// <summary>
        /// Task: "Write a query that will return the current balance for the user."
        /// </summary>
        public UserBalance GetUserBalance(Guid userId);

        /// <summary>
        /// User by DeleteUserTransactionsInCurrentMonth(Guid userId)
        /// </summary>
        public int DeleteUserTransactions(Guid userId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Task: "Write a command to delete all users' (parameter userId) transactions in the current month."
        /// (uses DeleteUserTransactions(Guid userId, DateTime startDate, DateTime endDate))
        /// </summary>
        public int DeleteUserTransactionsInCurrentMonth(Guid userId);

        public IEnumerable<TransactionInfo> GetUserTransactions(Guid userId);

    }
}
