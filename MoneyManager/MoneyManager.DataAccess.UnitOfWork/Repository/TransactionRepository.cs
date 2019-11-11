using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccess.Entity;

namespace MoneyManager.DataAccess.UnitOfWork.Repository
{
    public class TransactionRepository : Repository<Transaction>
    {
        protected DbSet<Transaction> TransactionSet { get => TypeSet; }
        public TransactionRepository(DbContext context) : base(context) {}

        public void DeleteByAssetId(Guid assetId, DateTime startDate, DateTime endDate)
        {
            TransactionSet.RemoveRange(
                TransactionSet.Where(transaction => transaction.Asset.Id == assetId && transaction.Date > startDate && transaction.Date < endDate)
            );
        }

        public void DeleteByAssetIdInCurrentMonth(Guid assetId)
        {
            var now = DateTime.Now;
            var from = new DateTime(now.Year, now.Month, 1);
            var to = from.AddDays(DateTime.DaysInMonth(now.Year, now.Month));
            DeleteByAssetId(assetId, from, to);
        }

        public IQueryable<Transaction> GetAllByAssetId(Guid assetId)
        {
            return TransactionSet
                .Where(t => t.Asset.Id == assetId)
                .OrderByDescending(t => t.Date);
        }
    }
}
