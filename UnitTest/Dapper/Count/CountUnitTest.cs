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
            User user = DapperExtension.Connection.QueryFirstOrDefault<User>("SELECT * FROM [user]");
            if (user != null)
            {
                var count = DapperExtension.Connection.RecordCount<User>($"where Id = '{user.Id}'");
                Assert.AreEqual(count, 1);
                count = DapperExtension.Connection.RecordCount<User>();
                Assert.GreaterOrEqual(count, 0);
            }
        }
    }
}
