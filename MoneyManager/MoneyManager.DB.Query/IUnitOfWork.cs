using System;
using System.Linq;
using MoneyManager.Entity;

namespace MoneyManager.Repository
{
    public struct UserMonthIncomeAndExpensesInfo
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
        /// Write a query to return the transaction list for the selected user (userId)
        /// ordered descending by Transaction.Date, then ordered ascending by Asset.Name and then ordered ascending by Category.Name.
        /// </summary>
        public IQueryable<TransactionInfo> GetUserTransactionInfos(Guid userId);

        /// <summary>
        /// Write a query that will return the total value of income and expenses for the selected period (parameters userId, startDate, endDate)
        /// Ordered by date and grouped by month.
        /// </summary>
        public IQueryable<UserMonthIncomeAndExpensesInfo> GetUserMonthIncomeAndExpensesInfos(Guid userId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// "Active category" - category that have transactions in the current month.
        /// </summary>
        public IQueryable<CategoryInfo> GetUserActiveCategoryInfos(Guid userId, CategoryType type);

        /// <summary>
        /// "Active category" - category that have transactions in the current month.
        /// </summary>
        public IQueryable<CategoryInfo> GetUserActiveCategoryInfos(Guid userId);

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

        /// <summary>
        /// Write a query that will return the total amount of all parent categories for the selected type of operation (Income or Expenses).
        /// The result should include only categories that have transactions in the current month. 
        /// In addition, you should order results descending by Transaction.Amount and then ordered them by Category.Name.
        /// </summary>
        public IQueryable<CategoryAmountInfo> GetUserCategoryAmountInfosInCurrentMonth(Guid userId, CategoryType categoryType);

        public AssetBalance GetAssetBalanceById(Guid assetId);

        /// <summary>
        /// Ordered by Transaction.Date and grouped by month.
        /// </summary>
        public IQueryable<AssetIncomeAndExpensesInfo> GetAssetIncomeAndExpensesInfos(Guid assetId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Write a query that will return the asset list for the selected user (userId) ordered by the asset’s name.
        /// </summary>
        public IQueryable<AssetBalance> GetUserAssetsBalances(Guid userId);

    }
}
