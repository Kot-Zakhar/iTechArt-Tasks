using System;
using System.Linq;
using MoneyManager.Entity;
using MoneyManager.Repository;
using System.Data.Entity;

namespace MoneyManager.MSSQLLocalDBRepository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
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
        
        public IQueryable<TransactionInfo> GetInfoByAssetId(Guid assetId, DateTime startDate, DateTime endDate)
        {
            return (from transaction in TransactionSet
                    where transaction.Asset.Id == assetId && transaction.Date > startDate && transaction.Date < endDate
                    select transaction).Select(transaction => new TransactionInfo(transaction));
        }
    }
}
