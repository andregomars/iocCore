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

        /// <summary>
        /// 混淆加密数据编号
        /// </summary>
        /// <param name="DataId">数据编号ID</param>
        /// <returns></returns>
        public static string EncryptID(object DataId)
        {
            string Data_Id = DataId.ToString();

            if (string.IsNullOrEmpty(Data_Id)) return null;

            int id = 0;

            string Gid = Guid.NewGuid().ToString().Replace("-", "");

            char[] G = Gid.ToCharArray();
            char[] U = Data_Id.ToString().ToCharArray();

            //数据字符长度
            int Len = Data_Id.ToString().Length;

            //奇/偶判断确定替换位置
            id = (Len % 2) == 0 ? 3 : 5;

            //依据长度奇/偶插入到不同位置
            for (int I = 0; I < U.Length; I++)
            {
                id += 2;
                G[id] = U[I];
            }

            //反转字符序列
            Array.Reverse(G);

            //开头处加入混淆字符
            //用16进制字符记录长度，并把长度记录到倒数第32个位子
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, new Random().Next(30)) + char.Parse(Convert.ToString(Len, 16)) + new string(G);
        }

        /// <summary>
        /// 解密数据编号,取回不成功返回"-1"
        /// </summary>
        /// <param name="DataId">从页面上接收到ID值</param>
        /// <returns>返回取回数据ID</returns>
        public static string DecryptID(object DataId)
        {
            if (DataId.Equals(null)) return null;

            string Data_Id = DataId.ToString();

            //未转换的GUID
            if (Data_Id.Split('-').Length == 5) return Data_Id;

            if (string.IsNullOrEmpty(Data_Id)) return null;

            int id = 0;
            string S = "";

            //反转得到正确的序列
            char[] U = Data_Id.ToCharArray();
            Array.Reverse(U);
            Data_Id = new string(U);

            //十六进制字符长度
            int Len = Convert.ToInt32(Data_Id.Substring(32, 1), 16);

            //依据长度奇/偶特性，取出字符
            id = (Len % 2) == 0 ? 3 : 5;
            for (int I = 0; I < Len; I++)
            {
                id += 2;
                S = S + Data_Id.Substring(id, 1);
            }
            return int.TryParse(S, out id) ? S : "-1";
        }
    }
}