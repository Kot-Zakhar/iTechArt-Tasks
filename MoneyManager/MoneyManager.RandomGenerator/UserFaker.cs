namespace MoneyManager.RandomGenerator
{
    public static class UserFaker
    {
        public static User CreateUser()
        {
            return new User()
            {
                Email = Faker.Internet.Email(),
                Name = Faker.Name.FullName(),
                // todo: generate valid sha256 hash and salt
                Salt = Faker.Lorem.GetFirstWord(),
                Hash = Faker.Internet.Email()
            };
        }

    }
}
