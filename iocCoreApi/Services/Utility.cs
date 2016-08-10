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

        //return encrypted ID
        public static string EncryptID(object DataId)
        {
            string Data_Id = DataId.ToString();

            if (string.IsNullOrEmpty(Data_Id)) return null;

            int id = 0;

            string Gid = Guid.NewGuid().ToString().Replace("-", "");

            char[] G = Gid.ToCharArray();
            char[] U = Data_Id.ToString().ToCharArray();

            int Len = Data_Id.ToString().Length;

            id = (Len % 2) == 0 ? 3 : 5;

            for (int I = 0; I < U.Length; I++)
            {
                id += 2;
                G[id] = U[I];
            }

            Array.Reverse(G);

            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, new Random().Next(30)) + char.Parse(Convert.ToString(Len, 16)) + new string(G);
        }

        //return decrypted ID
        public static string DecryptID(object DataId)
        {
            if (DataId.Equals(null)) return null;

            string Data_Id = DataId.ToString();

            if (Data_Id.Split('-').Length == 5) return Data_Id;

            if (string.IsNullOrEmpty(Data_Id)) return null;

            int id = 0;
            string S = "";

            char[] U = Data_Id.ToCharArray();
            Array.Reverse(U);
            Data_Id = new string(U);

            int Len = Convert.ToInt32(Data_Id.Substring(32, 1), 16);

            id = (Len % 2) == 0 ? 3 : 5;
            for (int I = 0; I < Len; I++)
            {
                id += 2;
                S = S + Data_Id.Substring(id, 1);
            }
            return int.TryParse(S, out id) ? S : "-1";
        }

        //return hashed password
        public static string HashPassword(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue;
            byte[] bytHash;
            string strPassword;
            md5 = new MD5CryptoServiceProvider();
            bytValue = System.Text.Encoding.UTF8.GetBytes(password);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            strPassword = Convert.ToBase64String(bytHash);
            return strPassword.ToLower();
        }
    }
}