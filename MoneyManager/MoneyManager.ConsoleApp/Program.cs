using MoneyManager.DataAccess.Entity;
using MoneyManager.DataAccess.UnitOfWork;
using MoneyManager.RandomGenerator;
using System;

namespace MoneyManager.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                User user = UserFaker.CreateUser();
                user = unitOfWork.UserRepository.Create(user);
                Console.WriteLine($"Adding a user : {user.Id} {user.Name} {user.Email}\n");
                unitOfWork.Commit();
            }
            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}
