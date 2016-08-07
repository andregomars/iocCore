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
    }
}