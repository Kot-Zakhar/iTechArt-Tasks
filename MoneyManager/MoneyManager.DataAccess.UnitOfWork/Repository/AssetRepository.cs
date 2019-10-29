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

        public IQueryable<Asset> GetUserAssets(Guid userId)
        {
            return AssetSet.Where(a => a.User.Id == userId);
        }
    }
}
