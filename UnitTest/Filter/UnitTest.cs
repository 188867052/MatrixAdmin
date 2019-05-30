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
        [Test]
        public void TestStringContainsFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                IQueryable<User> query = coreApiContext.User;
                query = query.AddStringContainsFilter(o => o.LoginName, "a");
                var a = coreApiContext.User.Where(o => o.LoginName.Contains("a")).Expression.ToString();
                var b = query.Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestStringContainsFilter);
            }
        }

        [Test]
        public void TestAddStringIsNullFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                IQueryable<User> query = coreApiContext.User;
                query = query.AddStringIsNullFilter(o => o.LoginName);
                var a = coreApiContext.User.Where(o => o.LoginName == null).Expression.ToString();
                var b = query.Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddStringIsNullFilter);
            }
        }

        [Test]
        public void TestAddStringIsEmptyFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                var a = coreApiContext.User.Where(o => o.LoginName == string.Empty).Expression.ToString();
                var b = coreApiContext.User.AddStringIsEmptyFilter(o => o.LoginName).Expression.ToString();

                Assert.AreEqual(a.Replace("String.Empty", "\"\""), b, UnitTestResource.TestAddStringIsEmptyFilter);
            }
        }

        [Test]
        public void TestAddIntegerEqualFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                IQueryable<User> query = coreApiContext.User;
                query = query.AddIntegerEqualFilter(1, o => o.Id);
                var a = coreApiContext.User.Where(o => o.Id == 1).Expression.ToString();
                var b = query.Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddIntegerEqualFilter);
            }
        }

        [Test]
        public void TestAddIntegerInArrayFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                var list = coreApiContext.User.Take(10).Select(o => o.Id).ToArray();
                var a = coreApiContext.User.Where(o => list.Contains(o.Id)).ToList();
                var b = coreApiContext.User.AddIntegerInArrayFilter(o => o.Id, list).ToList();

                Assert.AreEqual(a.Count, b.Count, UnitTestResource.TestAddIntegerInArrayFilter);
            }
        }

        [Test]
        public void TestAddStringInArrayFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                var list = coreApiContext.Role.Take(10).Select(o => o.Name).ToArray();
                var a = coreApiContext.Role.Where(o => list.Contains(o.Name)).ToList();
                var b = coreApiContext.Role.AddStringInArrayFilter(o => o.Name, list).ToList();

                Assert.AreEqual(a.Count, b.Count, UnitTestResource.TestAddStringInArrayFilter);
            }
        }

        [Test]
        public void TestAddStringEndsWithFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                var a = coreApiContext.User.Where(o => o.LoginName.EndsWith("a")).Expression.ToString();
                var b = coreApiContext.User.AddStringEndsWithFilter("a", o => o.LoginName).Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddStringEndsWithFilter);
            }
        }

        [Test]
        public void TestAddDateTimeBetweenFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                var list = coreApiContext.User.OrderBy(o => o.CreateTime).Take(10).Select(o => o.CreateTime).ToList();
                var a = coreApiContext.User.Where(o => o.CreateTime >= list.FirstOrDefault() && o.CreateTime <= list.LastOrDefault()).ToList();
                var b = coreApiContext.User.AddDateTimeBetweenFilter(list.FirstOrDefault(), list.LastOrDefault(), o => o.CreateTime).ToList();

                Assert.AreEqual(a.Count, b.Count);
            }
        }

        [Test]
        public void TestAddIntegerBetweenFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                var list = coreApiContext.User.OrderBy(o => o.CreateTime).Take(10).Select(o => o.Id).ToList();
                var a = coreApiContext.User.Where(o => o.Id >= list.FirstOrDefault() && o.Id <= list.LastOrDefault()).ToList();
                var b = coreApiContext.User.AddIntegerBetweenFilter(list.FirstOrDefault(), list.LastOrDefault(), o => o.Id).ToList();

                Assert.AreEqual(a.Count, b.Count);
            }
        }

        [Test]
        public void TestAddStringStartsWithFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                var a = coreApiContext.User.Where(o => o.LoginName.StartsWith("a")).Expression.ToString();
                var b = coreApiContext.User.AddStringStartsWithFilter("a", o => o.LoginName).Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddStringStartsWithFilter);
            }
        }

        [Test]
        public void TestAddStringEqualFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                var a = coreApiContext.User.Where(o => o.LoginName.Equals("a")).Expression.ToString();
                var b = coreApiContext.User.AddStringEqualFilter("a", o => o.LoginName).Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddStringEqualFilter);
            }
        }

        [Test]
        public void TestAddDateTimeGreaterThanOrEqualFilters()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                var a = coreApiContext.User.Where(o => o.CreateTime >= DateTime.Today).Expression.ToString().Replace("DateTime.Today", DateTime.Today.ToString());
                var b = coreApiContext.User.AddDateTimeGreaterThanOrEqualFilter(DateTime.Today, o => o.CreateTime).Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddDateTimeGreaterThanOrEqualFilters);
            }
        }

        [Test]
        public void TestAddDateTimeLessThanOrEqualFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                var a = coreApiContext.User.Where(o => o.CreateTime <= DateTime.Today).Expression.ToString().Replace("DateTime.Today", DateTime.Today.ToString());
                var b = coreApiContext.User.AddDateTimeLessThanOrEqualFilter(DateTime.Today, o => o.CreateTime).Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddDateTimeLessThanOrEqualFilter);
            }
        }

        [Test]
        public void TestAddStringNotNullFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                var a = coreApiContext.User.Where(o => o.LoginName != null).Expression.ToString();
                var b = coreApiContext.User.AddStringNotNullFilter(o => o.LoginName).Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddStringEqualFilter);
            }
        }

        [Test]
        public void TestAddBooleanFilter()
        {
            using (var coreApiContext = new CoreApiContext())
            {
                IQueryable<User> query = coreApiContext.User;
                query = query.AddBooleanFilter(o => o.IsEnable, false);
                var a = coreApiContext.User.Where(o => o.IsEnable == false).Expression.ToString();
                var b = query.Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddBooleanFilter);
            }
        }
    }
}
