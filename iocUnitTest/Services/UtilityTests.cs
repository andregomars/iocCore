using Microsoft.VisualStudio.TestTools.UnitTesting;
using iocCoreApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace iocCoreApi.Services.Tests
{
    [TestClass()]
    public class UtilityTests
    {
        [TestMethod()]
        public void GetMd5HashTest()
        {
            string input = "123";
            string hash = Utility.GetMd5Hash(input);
            Console.WriteLine("hash of {0} is: {1}", input, hash);
            Assert.AreEqual("202cb962ac59075b964b07152d234b70", hash);
        }

        [TestMethod()]
        public void GetTripleMd5HashTest()
        {
            string input = "1111";
            string hash = Utility.GetMd5Hash(input);
            hash = Utility.GetMd5Hash(hash);
            hash = Utility.GetMd5Hash(hash);
            Console.WriteLine("hash of {0} 3x is: {1}", input, hash);
            Assert.AreEqual("8be4177df4ec5dee8c8bc4f3b49d7a2d", hash);
        }

        [TestMethod()]
        public void VerifyTripleMd5HashTest()
        {
            string input = "1111";
            string hash = "8be4177df4ec5dee8c8bc4f3b49d7a2d";
            Assert.IsTrue(Utility.VerifyTripleMd5Hash(input, "8be4177df4ec5dee8c8bc4f3b49d7a2d"));
        }

        [TestMethod()]
        public void EncryptIDTest()
        {
            string input = "23";
            //output: e557a0d782f945acb1d3625b64d5c5d2225cdd130eafabe7r4d5n2ae564133
            string output = Utility.EncryptID(input);
            Console.WriteLine("{0} is encrypted to {1}", input, output);
        }

        [TestMethod()]
        public void DecryptIDTest()
        {
            string input = "80a58e02bfcc4e2889a9304b2fabc8b57f4cc3f3124f940";
            //output: 23
            string output = Utility.DecryptID(input);
            Console.WriteLine("{0} is decrypted to {1}", input, output);
        }

        [TestMethod()]
        public void HashPasswordTest()
        {
            string password = "1111";
            //output: tzxnvxlqr1gzhkl3zndoug==
            string hasedPass = Utility.HashPassword(password);
            Console.WriteLine("Password {0} is hashed to {1}", password, hasedPass);
        }
    }
}