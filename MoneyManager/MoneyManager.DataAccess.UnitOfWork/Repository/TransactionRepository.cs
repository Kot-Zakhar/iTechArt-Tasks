using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccess.Entity;

namespace MoneyManager.DataAccess.UnitOfWork.Repository
{
    public class TransactionRepository : Repository<Transaction>
    {
        protected DbSet<Transaction> TransactionSet { get => typeSet; }
        public TransactionRepository(DbContext context) : base(context) {}

        public int DeleteByAssetId(Guid assetId, DateTime startDate, DateTime endDate)
        {
            return TransactionSet.RemoveRange(
                from transaction in TransactionSet
                where transaction.Asset.Id == assetId && transaction.Date > startDate && transaction.Date < endDate
                select transaction
            ).Count();
        }

        public int DeleteByAssetIdInCurrentMonth(Guid assetId)
        {
            var now = DateTime.Now;
            var from = new DateTime(now.Year, now.Month, 1);
            var to = from.AddDays(DateTime.DaysInMonth(now.Year, now.Month));
            return DeleteByAssetId(assetId, from, to);
        }

        /// <summary>
        /// Ordering descending by Transaction.Date.
        /// </summary>
        public IQueryable<TransactionInfo> GetInfoByAssetId(Guid assetId, DateTime startDate, DateTime endDate)
        {
            return GetInfoByAssetId(assetId).Where(t => t.Date > startDate && t.Date < endDate);
        }

        public IQueryable<TransactionInfo> GetInfoByAssetId(Guid assetId)
        {
            return (from transaction in TransactionSet
                   where transaction.Asset.Id == assetId
                   select transaction).Select(transaction => new TransactionInfo(transaction));
        }
    }
}
