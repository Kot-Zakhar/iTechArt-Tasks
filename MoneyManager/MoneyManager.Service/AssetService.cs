using MoneyManager.DataAccess.Entity;
using MoneyManager.DataAccess.UnitOfWork;
using MoneyManager.Service.Model;
using System;
using System.Linq;

namespace MoneyManager.Service
{
    class AssetService
    {
        protected readonly UnitOfWork unitOfWork;

        public AssetService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<AssetInfo> GetUserAssetInfos(Guid userId)
        {
            return unitOfWork.AssetRepository.GetUserAssets(userId)
                .OrderBy(a => a.Name)
                .Select(a => new AssetInfo(a));
        }



        public AssetBalance GetAssetBalanceById(Guid assetId)
        {
            Asset asset = unitOfWork.AssetRepository.GetById(assetId);
            return unitOfWork.TransactionRepository.GetAll().Where(t => t.Asset.Id == assetId)
                .Aggregate(
                    0.0,
                    (balance, transaction) => balance + transaction.Amount,
                    balance => new AssetBalance()
                    {
                        AssetInfo = new AssetInfo(asset),
                        Balance = balance
                    }
                );
        }

        /// <summary>
        /// Ordered by Transaction.Date and grouped by month.
        /// </summary>
        public IQueryable<AssetIncomeAndExpensesInfo> GetAssetIncomeAndExpensesInfos(Guid assetId, DateTime startDate, DateTime endDate)
        {
            Asset asset = unitOfWork.AssetRepository.GetById(assetId);
            return unitOfWork.TransactionRepository.GetAllByAssetId(assetId)
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
            return unitOfWork.AssetRepository.GetUserAssets(userId)
                .Select(asset => GetAssetBalanceById(asset.Id))
                .OrderBy(b => b.AssetInfo.Name);
        }
    }
}
