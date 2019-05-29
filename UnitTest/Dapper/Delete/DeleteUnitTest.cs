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
            Log log = DapperExtension.Connection.FirstOrDefault<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.Delete<Log>(log.Id);
                Assert.AreEqual(count, 1);
            }

            log = DapperExtension.Connection.FirstOrDefault<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteAsync<Log>(log.Id);
                Assert.AreEqual(count.Result, 1);
            }
        }

        [Test]
        public void TestDeleteListWithWhereClause()
        {
            Log log = DapperExtension.Connection.FirstOrDefault<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteList<Log>($"Where id = {log.Id}");
                Assert.AreEqual(count, 1);
            }

            log = DapperExtension.Connection.FirstOrDefault<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteList<Log>(new { log.Id });
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestDeleteWithParameters()
        {
            Log log = DapperExtension.Connection.FirstOrDefault<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteList<Log>("where Id = @Id", new { log.Id });
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestDeleteByObject()
        {
            Log log = DapperExtension.Connection.FirstOrDefault<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.Delete(log);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestDeleteByObjectAsync()
        {
            Log log = DapperExtension.Connection.FirstOrDefault<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteAsync(log).Result;
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestDeleteByMultipleKeyObjectAsync()
        {
            MultiplePrimaryKeyTable entity = DapperExtension.Connection.QueryFirstOrDefault<MultiplePrimaryKeyTable>("SELECT top 1 * FROM [multiple_primary_key_table]");
            if (entity != null)
            {
                var count = DapperExtension.Connection.DeleteAsync(entity).Result;
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestDeleteByMultipleKeyObject()
        {
            MultiplePrimaryKeyTable entity = DapperExtension.Connection.QueryFirstOrDefault<MultiplePrimaryKeyTable>("SELECT top 1 * FROM [multiple_primary_key_table]");
            if (entity != null)
            {
                var count = DapperExtension.Connection.Delete(entity);
                Assert.AreEqual(count, 1);
            }
        }
    }
}
