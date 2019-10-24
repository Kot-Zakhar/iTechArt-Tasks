using System;
using MoneyManager.Entity;
using MoneyManager.DB;
using MoneyManager.RandomGenerator;
using MoneyManager.Repository;

namespace ConsoleApproach
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IRepository<User> userRepo = new MSSQLLocalDBRepository<User>(new MoneyManagerContext()))
            {
                User user = UserFaker.CreateUser();
                user = userRepo.Create(user);
                Console.WriteLine($"Adding a user: {user.Id}\n{user.Name}\n{user.Email}\n");
                userRepo.Save();
            }
            Console.WriteLine("hello");
            Console.ReadKey();
        }
    }
}
