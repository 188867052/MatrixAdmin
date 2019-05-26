using System;
using System.Linq;
using Core.Entity;
using Core.Extension;
using Core.UnitTest.Resource.Areas;
using Dapper;
using NUnit.Framework;

namespace Core.UnitTest.Filter
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
            var list = _coreApiContext.User.Take(10).Select(o => o.Id).ToArray();
            var a = _coreApiContext.User.Where(o => list.Contains(o.Id)).ToList();
            var b = _coreApiContext.User.AddIntegerInArrayFilter(o => o.Id, list).ToList();

            Assert.AreEqual(a.Count, b.Count, UnitTestResource.TestAddIntegerInArrayFilter);
        }

        [Test]
        public void TestAddStringInArrayFilter()
        {
            var list = _coreApiContext.Role.Take(10).Select(o => o.Name).ToArray();
            var a = _coreApiContext.Role.Where(o => list.Contains(o.Name)).ToList();
            var b = _coreApiContext.Role.AddStringInArrayFilter(o => o.Name, list).ToList();

            Assert.AreEqual(a.Count, b.Count, UnitTestResource.TestAddStringInArrayFilter);
        }

        [Test]
        public void TestAddStringEndsWithFilter()
        {
            var a = _coreApiContext.User.Where(o => o.LoginName.EndsWith("a")).Expression.ToString();
            var b = _coreApiContext.User.AddStringEndsWithFilter("a", o => o.LoginName).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddStringEndsWithFilter);
        }

        [Test]
        public void TestAddDateTimeBetweenFilter()
        {
            var list = _coreApiContext.User.OrderBy(o => o.CreateTime).Take(10).Select(o => o.CreateTime).ToList();
            var a = _coreApiContext.User.Where(o => o.CreateTime >= list.FirstOrDefault() && o.CreateTime <= list.LastOrDefault()).ToList();
            var b = _coreApiContext.User.AddDateTimeBetweenFilter(list.FirstOrDefault(), list.LastOrDefault(), o => o.CreateTime).ToList();

            Assert.AreEqual(a.Count, b.Count);
        }

        [Test]
        public void TestAddIntegerBetweenFilter()
        {
            var list = _coreApiContext.User.OrderBy(o => o.CreateTime).Take(10).Select(o => o.Id).ToList();
            var a = _coreApiContext.User.Where(o => o.Id >= list.FirstOrDefault() && o.Id <= list.LastOrDefault()).ToList();
            var b = _coreApiContext.User.AddIntegerBetweenFilter(list.FirstOrDefault(), list.LastOrDefault(), o => o.Id).ToList();

            Assert.AreEqual(a.Count, b.Count);
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
        public void TestAddDateTimeGreaterThanOrEqualFilters()
        {
            var a = _coreApiContext.User.Where(o => o.CreateTime >= DateTime.Today).Expression.ToString().Replace("DateTime.Today", DateTime.Today.ToString());
            var b = _coreApiContext.User.AddDateTimeGreaterThanOrEqualFilter(DateTime.Today, o => o.CreateTime).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddDateTimeGreaterThanOrEqualFilters);
        }

        [Test]
        public void TestAddDateTimeLessThanOrEqualFilter()
        {
            var a = _coreApiContext.User.Where(o => o.CreateTime <= DateTime.Today).Expression.ToString().Replace("DateTime.Today", DateTime.Today.ToString());
            var b = _coreApiContext.User.AddDateTimeLessThanOrEqualFilter(DateTime.Today, o => o.CreateTime).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddDateTimeLessThanOrEqualFilter);
        }

        [Test]
        public void TestAddStringNotNullFilter()
        {
            var a = _coreApiContext.User.Where(o => o.LoginName != null).Expression.ToString();
            var b = _coreApiContext.User.AddStringNotNullFilter(o => o.LoginName).Expression.ToString();

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
