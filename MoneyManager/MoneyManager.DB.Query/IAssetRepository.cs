using System;
using System.Linq;
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
    public struct AssetBalance
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

    public interface IAssetRepository : IRepository<Asset>
    {
        public IQueryable<AssetInfo> GetUserAssetInfos(Guid userId);
    }
}
