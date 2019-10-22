using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.RandomGenerator
{
    public class MoneyManagerFaker
    {
        private static Random random = new Random();

        public List<User> users = new List<User>();
        public List<Category> categories = new List<Category>();
        public List<Asset> assets = new List<Asset>();
        public List<Transaction> transactions = new List<Transaction>();

        public int userAmount = 10;
        public int assetPerUserAmount = 2;
        public int transactionPerAssetAmount = 250;

        public int rootCategoryAmount = 4;
        public int categoryGenerationAmount = 3;
        public int categoryChildPerGenerationAmount = 2;

        public void Clean()
        {
            users.Clear();
            categories.Clear();
            assets.Clear();
            transactions.Clear();
        }

        public void GenerateUsers()
        {
            for (var i = 0; i < userAmount; i++)
                users.Add(UserFaker.CreateUser());
        }

        public void GenerateAssetsForEachUsers()
        {
            users.ForEach(user =>
            {
                for (var i = 0; i < assetPerUserAmount; i++)
                    assets.Add(AssetFaker.CreateAsset(user));
            });
        }

        public void GenerateCategories()
        {
            for (var i = 0; i < rootCategoryAmount; i++)
                categories.AddRange(
                    CategoryFaker.CreateCategoryTree(
                        categoryChildPerGenerationAmount,
                        categoryGenerationAmount
                    )
                );
        }
        public void GenerateTransactionsForEachAsset()
        {
            assets.ForEach(asset =>
            {
                for (var i = 0; i < transactionPerAssetAmount; i++)
                    transactions.Add(
                        TransactionFaker.CreateTransaction(
                            asset,
                            categories[random.Next(categories.Count - 1)]
                        )
                    );
            });
        }

        public void Generate()
        {
            GenerateUsers();
            GenerateAssetsForEachUsers();
            GenerateCategories();
            GenerateTransactionsForEachAsset();
        }
    }
}
