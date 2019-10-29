using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccess.Context;
using MoneyManager.DataAccess.UnitOfWork.Repository;

namespace MoneyManager.DataAccess.UnitOfWork
{
    public class UnitOfWork
    {
        private static UnitOfWork instance = null;

        protected DbContext dbContext;
        public UserRepository UserRepository { get; protected set; }

        public CategoryRepository CategoryRepository { get; protected set; }

        public AssetRepository AssetRepository { get; protected set; }

        public TransactionRepository TransactionRepository { get; protected set; }

        private UnitOfWork()
        {
            dbContext = new MoneyManagerContext();
            UserRepository = new UserRepository(dbContext);
            CategoryRepository = new CategoryRepository(dbContext);
            AssetRepository = new AssetRepository(dbContext);
            TransactionRepository = new TransactionRepository(dbContext);
        }

        public static UnitOfWork GetInstance()
        {
            if (instance != null)
                instance = new UnitOfWork();
            return instance;
        }

        /// <summary>
        /// Commits all changes
        /// </summary>
        public void Commit()
        {
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Discards all changes that has not been commited
        /// </summary>
        public void RejectChanges()
        {
            foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }



        /// <summary>
        /// User by DeleteUserTransactionsInCurrentMonth(Guid userId)
        /// </summary>
        public int DeleteUserTransactions(Guid userId, DateTime startDate, DateTime endDate)
        {
            IQueryable<AssetInfo> assets = AssetRepository.GetUserAssetInfos(userId);
            int deleted = 0;
            foreach (var asset in assets)
                deleted += TransactionRepository.DeleteByAssetId(asset.Id, startDate, endDate);
            return deleted;
        }

        /// <summary>
        /// Task: "Write a command to delete all users' (parameter userId) transactions in the current month."
        /// (uses DeleteUserTransactions(Guid userId, DateTime startDate, DateTime endDate))
        /// </summary>
        public int DeleteUserTransactionsInCurrentMonth(Guid userId)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            DateTime firstDay = new DateTime(year, month, 1);
            return DeleteUserTransactions(userId, firstDay, firstDay.AddDays(days));
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

            return (from category in CategoryRepository.GetAll()
                    let activeTransactionsIds = GetUserTransactionInfos(userId).Where(t => t.Date >= firstDay && t.Date <= lastDay).Select(t => t.Id)
                    where activeTransactionsIds.Contains(category.Id)
                    select category)
                    .Select(c => new CategoryInfo(c));
        }

        public AssetBalance GetAssetBalanceById(Guid assetId)
        {
            return TransactionRepository.GetAll().Where(t => t.Asset.Id == assetId)
                .Aggregate(
                    0.0,
                    (balance, transaction) => balance + transaction.Amount,
                    balance => new AssetBalance()
                    {
                        AssetInfo = new AssetInfo(AssetRepository.GetById(assetId)),
                        Balance = balance
                    }
                );
        }

        /// <summary>
        /// Ordered by Transaction.Date and grouped by month.
        /// </summary>
        public IQueryable<AssetIncomeAndExpensesInfo> GetAssetIncomeAndExpensesInfos(Guid assetId, DateTime startDate, DateTime endDate)
        {
            Asset asset = AssetRepository.GetById(assetId);
            return TransactionRepository.GetAll().Where(t => t.Asset.Id == assetId && t.Date >= startDate && t.Date <= endDate)
                .GroupBy(t => new DateTime(t.Date.Year, t.Date.Month, 1))
                .Select(group => new AssetIncomeAndExpensesInfo()
                {
                    Month = group.Key.Month,
                    Year = group.Key.Year,
                    AssetId = new AssetInfo(asset),
                    Expenses = group.Where(t => t.Category.Type == CategoryType.Expense)
                                    .Aggregate(0.0, (value, transaction) => value + transaction.Amount),
                    Income = group.Where(t => t.Category.Type == CategoryType.Income)
                                  .Aggregate(0.0, (value, transaction) => value + transaction.Amount)
                });
        }

        /// <summary>
        /// Write a query that will return the asset list for the selected user (userId) ordered by the asset’s name.
        /// </summary>
        public IQueryable<AssetBalance> GetUserAssetsBalances(Guid userId)
        {
            return AssetRepository.GetUserAssetInfos(userId).Select(asset => GetAssetBalanceById(asset.Id));
        }

        /// <summary>
        /// Task: "Write a query that will return the current balance for the user."
        /// </summary>
        public UserBalance GetUserBalance(Guid userId)
        {
            return new UserBalance()
            {
                Balance = GetUserAssetsBalances(userId).Aggregate(0.0, (value, assetBalance) => value + assetBalance.Balance),
                UserInfo = new UserInfo(UserRepository.GetById(userId))
            };
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

            return GetUserTransactionInfos(userId)
                .Where(t => t.Date >= firstDay && t.Date <= lastDay && t.CategoryInfo.Type == categoryType && String.IsNullOrEmpty(t.CategoryInfo.ParentName))
                .GroupBy(t => t.CategoryInfo)
                .Select(transactionGroup => new CategoryAmountInfo()
                {
                    Category = transactionGroup.Key,
                    Amount = transactionGroup.Aggregate(0.0, (value, transaction) => value + transaction.Amount)
                });
        }

        /// <summary>
        /// Write a query that will return the total value of income and expenses for the selected period (parameters userId, startDate, endDate)
        /// Ordered by date and grouped by month.
        /// </summary>
        public IQueryable<UserMonthIncomeAndExpensesInfo> GetUserMonthIncomeAndExpensesInfos(Guid userId, DateTime startDate, DateTime endDate)
        {
            return GetUserTransactionInfos(userId)
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .GroupBy(t => new DateTime(t.Date.Year, t.Date.Month, 1))
                .Select(transactionGroup => new UserMonthIncomeAndExpensesInfo()
                {
                    Month = transactionGroup.Key.Month,
                    Year = transactionGroup.Key.Year,
                    UserInfo = new UserInfo(UserRepository.GetById(userId)),
                    Expenses = transactionGroup.Where(t => t.CategoryInfo.Type == CategoryType.Expense)
                                .Aggregate(0.0, (value, transaction) => value + transaction.Amount),
                    Income = transactionGroup.Where(t => t.CategoryInfo.Type == CategoryType.Income)
                                .Aggregate(0.0, (value, transaction) => value + transaction.Amount),

                });
        }

        /// <summary>
        /// Write a query to return the transaction list for the selected user (userId)
        /// ordered descending by Transaction.Date, then ordered ascending by Asset.Name and then ordered ascending by Category.Name.
        /// </summary>
        public IQueryable<TransactionInfo> GetUserTransactionInfos(Guid userId)
        {
            var assets = AssetRepository
                .GetAll()
                .Where(asset => asset.User.Id == userId);
            return TransactionRepository
                .GetAll()
                .Where(t => assets.Any(a => a.Id == t.Asset.Id))
                .Select(t => new TransactionInfo(t));
        }


        bool disposed = false;
        ~UnitOfWork()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                {
                    TransactionRepository.Dispose();
                    TransactionRepository = null;
                    UserRepository.Dispose();
                    UserRepository = null;
                    AssetRepository.Dispose();
                    AssetRepository = null;
                    CategoryRepository.Dispose();
                    CategoryRepository = null;
                    dbContext.Dispose();
                    dbContext = null;
                }
            disposed = true;
        }
    }
}
