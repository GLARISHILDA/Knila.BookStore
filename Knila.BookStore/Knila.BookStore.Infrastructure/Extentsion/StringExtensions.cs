using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Knila.BookStore.Infrastructure.Extentsion
{
    public static class StringExtensions
    {
        public static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            byte[] buff = new byte[size];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}