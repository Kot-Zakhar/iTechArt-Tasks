using System;
using System.Collections.Generic;
using System.Text;

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
            return new Transaction()
            {
                Category = category,
                Asset = asset,
                Amount = random.Next(MaxAmount) / 1000.0,
                Date = start.AddDays(random.Next(range)),
                Comment = Faker.Lorem.Sentence(5)
            };
        }
    }
}
