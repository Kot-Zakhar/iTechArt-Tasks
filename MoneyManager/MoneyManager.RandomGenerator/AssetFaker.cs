using MoneyManager.DataAccess.Entity;
using System.Collections.Generic;

namespace MoneyManager.RandomGenerator
{
    public static class AssetFaker
    {
        public static Asset CreateAsset(User user)
        {
            var asset = new Asset()
            {
                Name = Faker.Company.Name(),
                User = user,
                Transactions = new List<Transaction>()
            };
            user.Assets.Add(asset);
            return asset;
        }

        public static object ToPlainObject(Asset asset)
        {
            return new
            {
                asset.Id,
                asset.Name,
                UserId = asset.User.Id,
            };
        }
    }
}
