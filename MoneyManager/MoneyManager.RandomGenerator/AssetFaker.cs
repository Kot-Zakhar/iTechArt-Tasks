using MoneyManager.DataAccess.Entity;

namespace MoneyManager.RandomGenerator
{
    public static class AssetFaker
    {
        public static Asset CreateAsset(User user)
        {
            return new Asset()
            {
                Name = Faker.Company.Name(),
                User = user
            };
        }
    }
}
