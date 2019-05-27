using Core.Entity;
using Core.Extension.Dapper;
using Dapper;
using NUnit.Framework;

namespace Core.UnitTest.Dapper
{
    /// <summary>
    /// Api unit test.
    /// </summary>
    [TestFixture]
    public class DeleteUnitTest
    {
        [Test]
        public void TestDeleteById()
        {
            Log log = DapperExtension.Connection.QueryFirstOrDefault<Log>("SELECT * FROM [log]");
            if (log != null)
            {
                var count = DapperExtension.Connection.Delete<Log>(log.Id);
                Assert.AreEqual(count, 1);
            }

            log = DapperExtension.Connection.QueryFirstOrDefault<Log>("SELECT * FROM [log]");
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteAsync<Log>(log.Id);
                Assert.AreEqual(count.Result, 1);
            }
        }

        [Test]
        public void TestDeleteWithParameters()
        {
            Log log = DapperExtension.Connection.QueryFirstOrDefault<Log>("SELECT * FROM [log]");
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteList<Log>("where Id = @Id", new { log.Id });
                Assert.AreEqual(count, 1);
            }
        }
    }
}
