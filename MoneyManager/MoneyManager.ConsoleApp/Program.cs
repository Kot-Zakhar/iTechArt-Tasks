using MoneyManager.DataAccess.Entity;
using MoneyManager.DataAccess.UnitOfWork;
using MoneyManager.RandomGenerator;
using System;
using System.Linq;

namespace MoneyManager.ConsoleApp
{
    class Program
    {
        static void InitDBIfEmpty(UnitOfWork unitOfWork)
        {
            if (unitOfWork.UserRepository.GetAll().Any() || unitOfWork.CategoryRepository.GetAll().Any())
                return;

            var fakeGenerator = new MoneyManagerFaker();

            fakeGenerator.Generate();

            fakeGenerator.transactions.ForEach(t => unitOfWork.TransactionRepository.Create(t));
            fakeGenerator.categories.ForEach(c => unitOfWork.CategoryRepository.Create(c));
            fakeGenerator.assets.ForEach(a => unitOfWork.AssetRepository.Create(a));
            fakeGenerator.users.ForEach(u => unitOfWork.UserRepository.Create(u));

            unitOfWork.Commit();
        }
        static void Main(string[] args)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                InitDBIfEmpty(unitOfWork);

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
