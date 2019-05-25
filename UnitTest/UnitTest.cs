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
            Console.WriteLine("test fail 1");
            Assert.IsTrue(false, "test fail!");
        }

        [TestMethod]
        [TestCategory("MyUnitTest")]
        public void AddNumberTest()
        {
            int i = 5, j = 6;
            int result = 11;
            Console.WriteLine("test fail 2");
            Assert.AreNotEqual(result, i + j,"test fail");
        }
    }
}
