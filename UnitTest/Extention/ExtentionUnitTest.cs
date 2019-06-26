using System.Data;
using Core.Entity;
using Core.Extension;
using Core.Extension.Dapper;
using Xunit;

namespace Core.UnitTest.Extention
{
    /// <summary>
    /// Extention unit test.
    /// </summary>
    public class ExtentionUnitTest
    {
        [Fact]
        public void TestEmit()
        {
            var logs = DapperExtension.Connection.FindAll<Log>();
            DataTable dt = logs.ToDataTable();
            dt.ToList<Log>();

            Assert.True(true);
        }
    }
}
