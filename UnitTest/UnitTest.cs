using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass, TestCategory("MyUnitTest")]
    public class UnitTest
    {
        [TestMethod]
        [TestCategory("MyUnitTest")]
        public void TestMethod()
        {
            Console.WriteLine("this is a test message.");
            Assert.IsTrue(true, "test success!");
        }

        [TestMethod]
        [TestCategory("MyUnitTest")]
        public void AddNumberTest()
        {
            int i = 5, j = 6;
            int result = 11;
            Console.WriteLine("this is a test message2.");
            Assert.AreNotEqual(result, i + j,"test fail");
        }
    }
}
