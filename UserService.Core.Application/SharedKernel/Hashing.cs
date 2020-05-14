using System.Security.Cryptography;
using System.Text;

namespace UserService.Core.Application.SharedKernel
{
    public class Hashing
    {
        public static string HashPassword(string password)
        {
            var hasher = SHA384.Create();
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();
            foreach (var t in hash)
            {
                builder.Append(t.ToString("x2"));
            }
    
            return builder.ToString();
        }
    }
}