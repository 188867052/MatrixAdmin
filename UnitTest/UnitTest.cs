using System;
using System.Linq;
using Core.Entity;
using Core.Extension;
using Dapper;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using UnitTest.Resource.Areas;

namespace UnitTest
{
    [TestFixture]
    public class UnitTest
    {
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
            IQueryable<User> query = new CoreApiContext().User;
            query = query.AddStringContainsFilter(o => o.LoginName, "a");
            var a = query.Expression.ToString();
            var b = new CoreApiContext().User.Where(o => o.LoginName.Contains("a")).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestStringContainsFilter);
        }
    }
}
