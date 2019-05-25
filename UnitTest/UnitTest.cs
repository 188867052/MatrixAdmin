using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entity;
using Core.Extension;
using Dapper;
using NUnit.Framework;
using UnitTest.Resource.Areas;

namespace Core.UnitTest
{
    [TestFixture]
    public class UnitTest
    {
        private readonly CoreApiContext _coreApiContext = new CoreApiContext();

        [Test]
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
            var a = _coreApiContext.User.Where(o => o.LoginName.Contains("a")).Expression.ToString();
            var b = query.Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestStringContainsFilter);
        }

        [Test]
        public void TestAddStringIsNullFilter()
        {
            IQueryable<User> query = _coreApiContext.User;
            query = query.AddStringIsNullFilter(o => o.LoginName);
            var a = _coreApiContext.User.Where(o => o.LoginName == null).Expression.ToString();
            var b = query.Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddStringIsNullFilter);
        }

        [Test]
        public void TestAddStringIsEmptyFilter()
        {
            var a = _coreApiContext.User.Where(o => o.LoginName == "").Expression.ToString();
            var b = _coreApiContext.User.AddStringIsEmptyFilter(o => o.LoginName).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddStringIsEmptyFilter);
        }

        [Test]
        public void TestAddIntegerEqualFilter()
        {
            IQueryable<User> query = _coreApiContext.User;
            query = query.AddIntegerEqualFilter(1, o => o.Id);
            var a = _coreApiContext.User.Where(o => o.Id == 1).Expression.ToString();
            var b = query.Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddIntegerEqualFilter);
        }

        [Test]
        public void TestAddIntegerInArrayFilter()
        {
            var a = _coreApiContext.User.Where(o => new[] { 1, 2 ,3}.Contains(o.Id)).ToList();
            var b = _coreApiContext.User.AddIntegerInArrayFilter(o => o.Id, new[] { 1, 2,3 }).ToList();

            Assert.AreEqual(a.Count, b.Count, UnitTestResource.TestAddIntegerInArrayFilter);
        }

        [Test]
        public void TestAddStringEndsWithFilter()
        {
            var a = _coreApiContext.User.Where(o => o.LoginName.EndsWith("a")).Expression.ToString();
            var b = _coreApiContext.User.AddStringEndsWithFilter("a", o => o.LoginName).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddStringEndsWithFilter);
        }

        [Test]
        public void TestAddStringStartsWithFilter()
        {
            var a = _coreApiContext.User.Where(o => o.LoginName.StartsWith("a")).Expression.ToString();
            var b = _coreApiContext.User.AddStringStartsWithFilter("a", o => o.LoginName).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddStringStartsWithFilter);
        }

        [Test]
        public void TestAddStringEqualFilter()
        {
            var a = _coreApiContext.User.Where(o => o.LoginName.Equals("a")).Expression.ToString();
            var b = _coreApiContext.User.AddStringEqualFilter("a", o => o.LoginName).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddStringEqualFilter);
        }

        [Test]
        public void TestAddBooleanFilter()
        {
            IQueryable<User> query = _coreApiContext.User;
            query = query.AddBooleanFilter(o => o.IsEnable, false);
            var a = _coreApiContext.User.Where(o => o.IsEnable == false).Expression.ToString();
            var b = query.Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddBooleanFilter);
        }
    }
}
