using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Repository
{
    public struct TransactionInfo
    {
        public AssetInfo AssetInfo;
        public CategoryInfo CategoryInfo;
        public double Amount;
        public DateTime Date;
        public string Comment;
    }
    public interface ITransactionRepository : IRepository<Entity.Transaction>
    {
        public int DeleteByUserIdInCurrentMonth(Guid userId);
        public int DeleteByUserId(Guid userId, DateTime from, DateTime to);

        public IEnumerable<TransactionInfo> GetInfoByUserId(Guid userId);
        public IEnumerable<TransactionInfo> GetInfoByUserIdSorted(Guid userId, IComparer<TransactionInfo> comparer);

        /// <summary>
        /// Ordering descending by Transaction.Date, then ascending by Asset.Name and then ascending by Category.Name.
        /// </summary>
        public IEnumerable<TransactionInfo> GetInfoByUserIdSorted(Guid userId);
    }
}
