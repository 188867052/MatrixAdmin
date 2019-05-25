using System;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void TestMethod()
        {
            Console.WriteLine("test fail 1");
            Assert.IsTrue(false, "test fail!");
        }

        [Test]
        public void AddNumberTest()
        {
            int i = 5, j = 6;
            int result = 11;
            Console.WriteLine("test fail 2");
            Assert.AreNotEqual(result, i + j,"test fail");
        }
    }
}
