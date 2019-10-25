using System;
using System.Collections.Generic;
using MoneyManager.Entity;

namespace MoneyManager.Repository
{
    public struct AssetInfo
    {
        public Guid Id;
        public string Name;

        public AssetInfo(Asset asset)
        {
            Id = asset.Id;
            Name = asset.Name;
        }
    }
    public struct AssetBalanceInfo
    {
        public AssetInfo AssetInfo;
        public double Balance;
    }
    public struct AssetIncomeAndExpensesInfo
    {
        public AssetInfo AssetId;
        public double Income;
        public double Expenses;
        public int Month;
        public int Year;
    }

    public interface IAssetRepository : IRepository<Entity.Asset>
    {
        public AssetInfo GetAssetInfoById(Guid assetId);

        public IEnumerable<AssetInfo> GetInfosByUserId(Guid userId);

        /// <summary>
        /// Write a query that will return the asset list for the selected user (userId) ordered by the asset’s name.
        /// </summary>
        public IEnumerable<AssetInfo> GetInfosByUserIdSortedByAssetName(Guid userId);

        /// <summary>
        /// Ordered by Transaction.Date and grouped by month.
        /// </summary>
        public IEnumerable<AssetIncomeAndExpensesInfo> GetAssetIncomeAndExpensesInfos(Guid assetId, DateTime from, DateTime to);
    }
}
