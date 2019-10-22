using System;
using MoneyManager;

namespace ConsoleApproach
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MoneyManagerContext())
            {
                var generator = new MoneyManager.RandomGenerator.MoneyManagerFaker();
                generator.Generate();

                context.Users.AddRange(generator.users);
                context.Assets.AddRange(generator.assets);
                context.Categories.AddRange(generator.categories);
                context.Transactions.AddRange(generator.transactions);

                context.SaveChanges();
            }

            Console.WriteLine("hello");
            Console.ReadKey();
        }
    }
}
