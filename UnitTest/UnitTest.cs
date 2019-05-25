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
            Assert.IsTrue(true,"test success!this is a test message.");
        }
        
        [TestMethod]
        [TestCategory("MyUnitTest")]
        public void AddNumberTest()
        {
            int i = 5, j = 6;
            int result = 11;

            Assert.AreEqual(result, i + j);
        }
    }
}
