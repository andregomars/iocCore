using System;
using System.Security.Cryptography;
using System.Text;

namespace iocCoreApi.Services
{
    public static class Utility
    {
        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(input);
                byte[] hashedBytes = MD5CryptoServiceProvider.Create().ComputeHash(asciiBytes);
                string hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                // Return the hexadecimal string.
                return hashedString;
            }
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(string input, string hash, int times)
        {
            string hashOfInput = GetMd5Hash(input);

            using (MD5 md5Hash = MD5.Create())
            {
                // Hash the input based on times
                times--;
                while (times > 0)
                {
                    hashOfInput = GetMd5Hash(hashOfInput);
                    times--;
                }
                

                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Verify a hash against a string 3x.
        public static bool VerifyTripleMd5Hash(string input, string hash)
        {
            return VerifyMd5Hash(input, hash, 3);
        }
    }
}