using System;
using MoneyManager.Entity;
using MoneyManager.DB;
using MoneyManager.RandomGenerator;

namespace ConsoleApproach
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MoneyManagerContext())
            {
                User user = UserFaker.CreateUser();
                Console.WriteLine($"Adding a user: {user.Id}\n{user.Name}\n{user.Email}\n");
                context.Users.Add(user);
                context.SaveChanges();
            }
            Console.WriteLine("hello");
            Console.ReadKey();
        }
    }
}
