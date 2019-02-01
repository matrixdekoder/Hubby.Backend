using System;
using System.Security.Cryptography;

namespace Core.Infrastructure
{
    public class PasswordComputer : IPasswordComputer
    {
        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int HashIterations = 10000;

        public string Hash(string password)
        {
            // Generate random salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // Hash password
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, HashIterations);
            var hash = pbkdf2.GetBytes(HashSize);
            var hashBytes = new byte[SaltSize + HashSize];

            // Prepend salt to hash
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        public bool Compare(string inputPassword, string storedPassword)
        {
            // Retrieve stored salt
            var storedPasswordHash = Convert.FromBase64String(storedPassword);
            var storedPasswordSalt = new byte[SaltSize];
            Array.Copy(storedPasswordHash, 0, storedPasswordSalt, 0, SaltSize);

            // Salt input password
            var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, storedPasswordSalt, HashIterations);
            var inputPasswordHash = pbkdf2.GetBytes(HashSize);

            // Compare to stored hash, salt excluded
            bool result = true;

            for (var i = 0; i < HashSize; i++)
            {
                if (storedPassword[i + SaltSize] != inputPasswordHash[i])
                    result = false;
            }

            return result;
        }
    }
}