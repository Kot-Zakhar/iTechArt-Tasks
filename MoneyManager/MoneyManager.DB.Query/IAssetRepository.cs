using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Repository
{
    public struct AssetInfo
    {
        public Guid Id;
        public string Name;
        public double Balance;
    }
    public interface IAssetRepository : IRepository<Entity.Asset>
    {
        public AssetInfo GetAssetInfoById(Guid assetId);

        public IEnumerable<AssetInfo> GetInfosByUserId(Guid userId);
        public IEnumerable<AssetInfo> GetInfosByUserIdSorted(Guid userId, IComparer<AssetInfo> comparer);
        public IEnumerable<AssetInfo> GetInfosByUserIdSortedByAssetName(Guid userId);
    }
}
