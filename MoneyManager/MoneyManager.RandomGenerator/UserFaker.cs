using System;
using System.Text;
using System.Security.Cryptography;

namespace MoneyManager.RandomGenerator
{
    public static class UserFaker
    {
        private static RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();
        private static int saltLength = 1024;
        
        public static User CreateUser()
        {
            byte[] saltBytes = new byte[saltLength * 2];
            csp.GetBytes(saltBytes);
            string salt = Convert.ToBase64String(saltBytes);
            string name = Faker.Name.FullName();
            string password = name;
            string hash = Convert.ToBase64String(
                SHA512.Create().ComputeHash(
                    Encoding.Unicode.GetBytes(salt + password)
                )
            );
            
            return new User()
            {
                Email = Faker.Internet.Email(name),
                Name = name,
                Salt = salt,
                Hash = hash
            };
        }

    }
}
