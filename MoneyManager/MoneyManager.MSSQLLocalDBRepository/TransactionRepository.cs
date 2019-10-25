using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MoneyManager.Entity;
using MoneyManager.Repository;
using System.Data.Entity;

namespace MoneyManager.MSSQLLocalDBRepository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext context) : base(context) {}

        public int DeleteByAssetId(Guid assetId, DateTime startDate, DateTime endDate)
        {
            return base.typeSet.RemoveRange(
                from transaction in base.typeSet
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

        public IEnumerable<TransactionInfo> GetInfoByAssetId(Guid assetId, DateTime startDate, DateTime endDate)
        {
            return (from transaction in base.typeSet
                    where transaction.Asset.Id == assetId && transaction.Date > startDate && transaction.Date < endDate
                    select transaction).Select(transaction => new TransactionInfo(transaction));
        }

    }
}
