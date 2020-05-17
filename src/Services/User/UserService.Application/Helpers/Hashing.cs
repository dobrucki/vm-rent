using System;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace UserService.Application.Helpers
{
    public static class Hashing
    {
        public static string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var salt = new byte[16];
            new RNGCryptoServiceProvider().GetNonZeroBytes(salt);
            var argon2Id = new Argon2id(bytes)
            {
                Salt = salt,
                Iterations = 4, 
                MemorySize = 8192
            };
            var hash = argon2Id.GetBytes(128);
            return Convert.ToBase64String(hash);
        }
    }
}    