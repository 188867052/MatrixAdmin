using System.Threading.Tasks;
using Core.Entity;
using Core.Extension.Dapper;
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
        public async Task TestRecordCountAsync()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                var count = DapperExtension.Connection.RecordCount<User>($"where Id = '{user.Id}'");
                Assert.AreEqual(count, 1);
                count = DapperExtension.Connection.RecordCount<User>();
                Assert.GreaterOrEqual(count, 0);
                count = await DapperExtension.Connection.RecordCountAsync<User>();
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public async Task TestRecordCount()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                int count = await DapperExtension.Connection.RecordCountAsync<User>();
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task TestRecordCountByObjectAsync(int id)
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                int count = await DapperExtension.Connection.RecordCountAsync<User>(new { id });
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public async Task TestRecordCountByObjectAsyncEgnoreCaseAsync()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                int count = await DapperExtension.Connection.RecordCountAsync<User>(new { ID = 10 });
                Assert.GreaterOrEqual(count, 0);
            }
        }
    }
}
