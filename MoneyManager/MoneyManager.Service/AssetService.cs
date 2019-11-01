using MoneyManager.DataAccess.Entity;
using MoneyManager.DataAccess.UnitOfWork;
using MoneyManager.Service.Model;
using System;
using System.Linq;

namespace MoneyManager.Service
{
    class AssetService
    {
        protected readonly UnitOfWork UnitOfWork;

        public AssetService(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IQueryable<AssetInfo> GetUserAssetInfos(Guid userId)
        {
            return UnitOfWork.AssetRepository.GetUserAssets(userId)
                .OrderBy(a => a.Name)
                .Select(a => new AssetInfo(a));
        }



        public AssetBalance GetAssetBalanceById(Guid assetId)
        {
            Asset asset = UnitOfWork.AssetRepository.GetById(assetId);
            return new AssetBalance()
            {
                AssetInfo = new AssetInfo(asset),
                Balance = UnitOfWork.TransactionRepository.GetAll()
                            .Where(t => t.Asset.Id == assetId)
                            .Sum(t => t.Amount * (t.Category.Type == CategoryType.Expense ? -1 : 1))
            };
        }

        /// <summary>
        /// Ordered by Transaction.Date and grouped by month.
        /// </summary>
        public IQueryable<AssetIncomeAndExpensesInfo> GetAssetIncomeAndExpensesInfos(Guid assetId, DateTime startDate, DateTime endDate)
        {
            Asset asset = UnitOfWork.AssetRepository.GetById(assetId);
            return UnitOfWork.TransactionRepository.GetAllByAssetId(assetId)
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .GroupBy(t => new DateTime(t.Date.Year, t.Date.Month, 1))
                .Select(group => new AssetIncomeAndExpensesInfo()
                {
                    Month = group.Key.Month,
                    Year = group.Key.Year,
                    Asset = new AssetInfo(asset),
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
            return UnitOfWork.AssetRepository.GetUserAssets(userId)
                .Select(asset => GetAssetBalanceById(asset.Id))
                .OrderBy(b => b.AssetInfo.Name);
        }
    }
}
