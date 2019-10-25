using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MoneyManager.Entity;
using MoneyManager.Repository;

namespace MoneyManager.MSSQLLocalDBRepository
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        public AssetRepository(DbContext context) : base(context) {}

        public IEnumerable<AssetIncomeAndExpensesInfo> GetAssetIncomeAndExpensesInfos(Guid assetId, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public AssetInfo GetAssetInfoById(Guid assetId)
        {
            return (from asset in base.typeSet
                    where asset.Id == assetId
                    select asset)
                    .Select(asset => 
                        new AssetInfo()
                        {
                            Id = asset.Id,
                            Name = asset.Name
                        }
                    )
                    .FirstOrDefault();
        }

        public IEnumerable<AssetInfo> GetInfosByUserId(Guid userId)
        {
            return (from asset in base.typeSet
                    where asset.User.Id == userId
                    select asset)
                    .Select(asset =>
                        new AssetInfo()
                        {
                            Id = asset.Id,
                            Name = asset.Name
                        }
                    );
        }

        public IEnumerable<AssetInfo> GetInfosByUserIdSorted(Guid userId, IComparer<AssetInfo> comparer)
        {
            return GetInfosByUserId(userId).OrderBy(asset => asset, comparer);
        }

        public IEnumerable<AssetInfo> GetInfosByUserIdSortedByAssetName(Guid userId)
        {
            return GetInfosByUserId(userId).OrderBy(asset => asset.Name);
        }
    }
}
