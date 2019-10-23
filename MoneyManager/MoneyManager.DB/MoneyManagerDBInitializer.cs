using System;
using System.Data.Entity;

namespace MoneyManager.DB
{
    public class MoneyManagerDBInitializer : DropCreateDatabaseIfModelChanges<MoneyManagerContext>
    {
        protected override void Seed(MoneyManagerContext context)
        {
            base.Seed(context);

            var generator = new RandomGenerator.MoneyManagerFaker();

            generator.Generate();

            context.Users.AddRange(generator.users);
            context.Assets.AddRange(generator.assets);
            context.Categories.AddRange(generator.categories);
            context.Transactions.AddRange(generator.transactions);

            context.SaveChanges();
        }
    }
}
