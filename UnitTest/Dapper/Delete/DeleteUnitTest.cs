using System.Threading.Tasks;
using Core.Entity;
using Core.Extension.Dapper;
using NUnit.Framework;

namespace Core.UnitTest.Dapper.Delete
{
    /// <summary>
    /// Api unit test.
    /// </summary>
    [TestFixture]
    public class DeleteUnitTest
    {
        [Test]
        public async Task TestDeleteById()
        {
            Log log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.Delete<Log>(log.Id);
                Assert.AreEqual(count, 1);
            }

            log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                int count = await DapperExtension.Connection.DeleteAsync<Log>(log.Id);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestDeleteListWithWhereClause()
        {
            Log log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteList<Log>($"Where id = {log.Id}");
                Assert.AreEqual(count, 1);
            }

            log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteList<Log>(new { log.Id });
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public async Task TestDeleteWithParameters()
        {
            Log log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteList<Log>("where Id = @Id", new { log.Id });
                Assert.AreEqual(count, 1);
            }

            log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                var count = await DapperExtension.Connection.DeleteListAsync<Log>("where Id = @Id", new { log.Id });
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestDeleteByObject()
        {
            Log log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.Delete(log);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public async Task TestDeleteByObjectAsync()
        {
            Log log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                int count = await DapperExtension.Connection.DeleteAsync(log);
                Assert.AreEqual(count, 1);
            }

            log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                int count = DapperExtension.Connection.Delete(log);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public async Task TestDeleteByMultipleKeyObjectAsync()
        {
            MultiplePrimaryKeyTable entity = DapperExtension.Connection.QueryFirst<MultiplePrimaryKeyTable>();
            if (entity != null)
            {
                var count = await DapperExtension.Connection.DeleteAsync(entity);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestDeleteByMultipleKeyObject()
        {
            MultiplePrimaryKeyTable entity = DapperExtension.Connection.QueryFirst<MultiplePrimaryKeyTable>();
            if (entity != null)
            {
                var count = DapperExtension.Connection.Delete(entity);
                Assert.AreEqual(count, 1);
            }
        }
    }
}
