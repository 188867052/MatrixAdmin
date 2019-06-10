using System.Data;
using Core.Entity;
using Core.Extension;
using Core.Extension.Dapper;
using NUnit.Framework;

namespace Core.UnitTest.Extention
{
    /// <summary>
    /// Extention unit test.
    /// </summary>
    [TestFixture]
    public class ExtentionUnitTest
    {
        [Test]
        public void TestEmit()
        {
            var logs = DapperExtension.Connection.FindAll<Log>();
            DataTable dt = logs.ToDataTable();
            dt.ToList<Log>();

            Assert.True(true);
        }
    }
}
