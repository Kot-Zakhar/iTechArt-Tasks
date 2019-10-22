using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.RandomGenerator
{
    public static class TransactionFaker
    {
        private static Random random = new Random();
        public static int MaxAmount { get; set; } = 10000000;
        public static Transaction CreateTransaction(Asset asset, Category category)
        {
            return new Transaction()
            {
                Category = category,
                Asset = asset,
                Amount = random.Next(MaxAmount) / 1000.0,
                Date = Faker.DateOfBirth.Next(),
                Comment = Faker.Lorem.Sentence(5)
            };
        }
    }
}
