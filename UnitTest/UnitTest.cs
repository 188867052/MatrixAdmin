using System;
using System.Linq;
using Core.Entity;
using Core.Extension;
using Dapper;
using NUnit.Framework;
using UnitTest.Resource.Areas;

namespace UnitTest
{
    [TestFixture]
    public class UnitTest
    {
        private readonly CoreApiContext _coreApiContext = new CoreApiContext();

        [Test(Description = "测试数据库是否连接")]
        public void TestDataBaseConnection()
        {
            Exception exception = null;
            try
            {
                string sql = $"UPDATE [User] SET IsDeleted = @IsDeleted WHERE IsDeleted = @IsDeleted";
                CoreApiContext.Dapper.Execute(sql, new { IsDeleted = false });
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsNull(exception, UnitTestResource.TestDataBaseConnection);
        }

        [Test]
        public void TestStringContainsFilter()
        {
            IQueryable<User> query = _coreApiContext.User;
            query = query.AddStringContainsFilter(o => o.LoginName, "a");
            var a = query.Expression.ToString();
            var b = _coreApiContext.User.Where(o => o.LoginName.Contains("a")).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestStringContainsFilter);
        }

        [Test]
        public void TestAddIntegerEqualFilter()
        {
            IQueryable<User> query = _coreApiContext.User;
            query = query.AddIntegerEqualFilter(1, o => o.Id);
            var a = query.Expression.ToString();
            var b = _coreApiContext.User.Where(o => o.Id == 1).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddIntegerEqualFilter);
        }

        [Test]
        public void TestAddBooleanFilter()
        {
            IQueryable<User> query = _coreApiContext.User;
            query = query.AddBooleanFilter(o => o.IsEnable, true);
            var a = query.Expression.ToString();
            var b = _coreApiContext.User.Where(o => o.IsEnable == true).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddBooleanFilter);
        }
    }
}
