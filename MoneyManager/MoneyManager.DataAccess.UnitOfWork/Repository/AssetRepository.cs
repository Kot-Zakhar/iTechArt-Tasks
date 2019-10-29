using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccess.Entity;

namespace MoneyManager.DataAccess.UnitOfWork.Repository
{
    public class AssetRepository : Repository<Asset>
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
