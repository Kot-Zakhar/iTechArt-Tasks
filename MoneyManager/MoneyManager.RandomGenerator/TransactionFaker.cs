using System;
using MoneyManager.DataAccess.Entity;

namespace MoneyManager.RandomGenerator
{
    public static class TransactionFaker
    {
        private static Random random = new Random();
        private static DateTime start = new DateTime(2000, 1, 1);
        private static int range = (DateTime.Today - start).Days;

        public static int MaxAmount { get; set; } = 10000000;
        public static Transaction CreateTransaction(Asset asset, Category category)
        {
            var transaction = new Transaction()
            {
                Category = category,
                Asset = asset,
                Amount = random.Next(MaxAmount) / 1000.0,
                Date = start.AddDays(random.Next(range)),
                Comment = Faker.Lorem.Sentence(5)
            };
            asset.Transactions.Add(transaction);
            category.Transactions.Add(transaction);
            return transaction;
        }

        public static object ToPlainObject(Transaction transaction)
        {
            return new
            {
                transaction.Id,
                transaction.Amount,
                transaction.Date,
                transaction.Comment,
                CategoryId = transaction.Category.Id,
                AssetId = transaction.Asset.Id
            };
        }
    }
}
