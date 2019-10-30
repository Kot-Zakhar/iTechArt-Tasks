﻿using MoneyManager.DataAccess.Entity;
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

            var faker = new MoneyManagerFaker();
            faker.Generate();

            faker.transactions.ForEach(t => unitOfWork.TransactionRepository.Create(t));
            faker.categories.ForEach(c => unitOfWork.CategoryRepository.Create(c));
            faker.assets.ForEach(a => unitOfWork.AssetRepository.Create(a));
            faker.users.ForEach(u => unitOfWork.UserRepository.Create(u));
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
