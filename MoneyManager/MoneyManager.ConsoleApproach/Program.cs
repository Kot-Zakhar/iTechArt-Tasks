using System;
using MoneyManager.Entity;
using MoneyManager.MSSQLLocalDBRepository;
using MoneyManager.RandomGenerator;
using MoneyManager.Repository;

namespace ConsoleApproach
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                User user = UserFaker.CreateUser();
                user = unitOfWork.UserRepository.Create(user);
                Console.WriteLine($"Adding a user: {user.Id}\n{user.Name}\n{user.Email}\n");
                unitOfWork.Commit();
            }
            Console.WriteLine("hello");
            Console.ReadKey();
        }
    }
}
