using System;
using System.Linq;
using MoneyManager.Entity;

namespace MoneyManager.Repository
{
    public struct TransactionInfo
    {
        public Guid Id;
        public AssetInfo AssetInfo;
        public CategoryInfo CategoryInfo;
        public double Amount;
        public DateTime Date;
        public string Comment;

        public TransactionInfo(Transaction transaction)
        {
            AssetInfo = new AssetInfo(transaction.Asset);
            CategoryInfo = new CategoryInfo(transaction.Category);
            Id = transaction.Id;
            Amount = transaction.Amount;
            Date = transaction.Date;
            Comment = transaction.Comment;
        }
    }

    public interface ITransactionRepository : IRepository<Transaction>
    {
        public int DeleteByAssetIdInCurrentMonth(Guid assetId);
        public int DeleteByAssetId(Guid assetId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Ordering descending by Transaction.Date.
        /// </summary>
        public IQueryable<TransactionInfo> GetInfoByAssetId(Guid assetId, DateTime startDate, DateTime endDate);

        public IQueryable<TransactionInfo> GetInfoByAssetId(Guid assetId);

    }
}
