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
        protected DbSet<Asset> AssetSet { get => typeSet; }
        public AssetRepository(DbContext context) : base(context) {}

        public IQueryable<AssetInfo> GetUserAssetInfos(Guid userId)
        {
            return (from asset in AssetSet
                    where asset.User.Id == userId
                    select asset)
                    .Select(a => new AssetInfo(a));
        }
    }
}
