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
                Console.WriteLine("Generated.");
                Console.WriteLine("Adding users to db...");
                context.Users.AddRange(generator.users);
                Console.WriteLine("Adding assets to db...");
                context.Assets.AddRange(generator.assets);
                Console.WriteLine("Adding categories to db...");
                context.Categories.AddRange(generator.categories);
                Console.WriteLine("Adding transactions to db...");
                context.Transactions.AddRange(generator.transactions);

                context.SaveChanges();
            }

            Console.WriteLine("hello");
            Console.ReadKey();
        }
    }
}
