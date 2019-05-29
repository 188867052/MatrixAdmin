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
    public class CountUnitTest
    {
        [Test]
        public void TestRecordCount()
        {
            User user = DapperExtension.Connection.FirstOrDefault<User>();
            if (user != null)
            {
                var count = DapperExtension.Connection.RecordCount<User>($"where Id = '{user.Id}'");
                Assert.AreEqual(count, 1);
                count = DapperExtension.Connection.RecordCount<User>();
                Assert.GreaterOrEqual(count, 0);
                count = DapperExtension.Connection.RecordCountAsync<User>().Result;
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public void TestRecordCountAsync()
        {
            User user = DapperExtension.Connection.FirstOrDefault<User>();
            if (user != null)
            {
                int count = DapperExtension.Connection.RecordCountAsync<User>().Result;
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public void TestRecordCountByObjectAsync()
        {
            User user = DapperExtension.Connection.FirstOrDefault<User>();
            if (user != null)
            {
                int count = DapperExtension.Connection.RecordCountAsync<User>(new { Id = 10 }).Result;
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public void TestRecordCountByObjectAsyncEgnoreCase()
        {
            User user = DapperExtension.Connection.FirstOrDefault<User>();
            if (user != null)
            {
                int count = DapperExtension.Connection.RecordCountAsync<User>(new { ID = 10 }).Result;
                Assert.GreaterOrEqual(count, 0);
            }
        }
    }
}
