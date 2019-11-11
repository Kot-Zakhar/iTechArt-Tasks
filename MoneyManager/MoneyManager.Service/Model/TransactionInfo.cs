using MoneyManager.DataAccess.Entity;
using System;

namespace MoneyManager.Service.Model
{
    public class TransactionInfo
    {
        public Guid Id { get; set; }
        public AssetInfo AssetInfo { get; set; }
        public CategoryInfo CategoryInfo { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

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
}
