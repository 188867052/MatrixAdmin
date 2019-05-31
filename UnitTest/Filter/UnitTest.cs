using System;
using System.Linq;
using Core.Entity;
using Core.Extension;
using Core.UnitTest.Resource.Areas;
using NUnit.Framework;

namespace Core.UnitTest.Filter
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void TestStringContainsFilter()
        {
            using (var context = new CoreContext())
            {
                IQueryable<User> query = context.User;
                query = query.AddStringContainsFilter(o => o.LoginName, "a");
                var a = context.User.Where(o => o.LoginName.Contains("a")).Expression.ToString();
                var b = query.Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestStringContainsFilter);
            }
        }

        [Test]
        public void TestAddStringIsNullFilter()
        {
            using (var context = new CoreContext())
            {
                IQueryable<User> query = context.User;
                query = query.AddStringIsNullFilter(o => o.LoginName);
                var a = context.User.Where(o => o.LoginName == null).Expression.ToString();
                var b = query.Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddStringIsNullFilter);
            }
        }

        [Test]
        public void TestAddStringIsEmptyFilter()
        {
            using (var context = new CoreContext())
            {
                var a = context.User.Where(o => o.LoginName == string.Empty).Expression.ToString();
                var b = context.User.AddStringIsEmptyFilter(o => o.LoginName).Expression.ToString();

                Assert.AreEqual(a.Replace("String.Empty", "\"\""), b, UnitTestResource.TestAddStringIsEmptyFilter);
            }
        }

        [Test]
        public void TestAddIntegerEqualFilter()
        {
            using (var context = new CoreContext())
            {
                IQueryable<User> query = context.User;
                query = query.AddIntegerEqualFilter(1, o => o.Id);
                var a = context.User.Where(o => o.Id == 1).Expression.ToString();
                var b = query.Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddIntegerEqualFilter);
            }
        }

        [Test]
        public void TestAddIntegerInArrayFilter()
        {
            using (var context = new CoreContext())
            {
                var list = context.User.Take(10).Select(o => o.Id).ToArray();
                var a = context.User.Where(o => list.Contains(o.Id)).ToList();
                var b = context.User.AddIntegerInArrayFilter(o => o.Id, list).ToList();

                Assert.AreEqual(a.Count, b.Count, UnitTestResource.TestAddIntegerInArrayFilter);
            }
        }

        [Test]
        public void TestAddStringInArrayFilter()
        {
            using (var context = new CoreContext())
            {
                var list = context.Role.Take(10).Select(o => o.Name).ToArray();
                var a = context.Role.Where(o => list.Contains(o.Name)).ToList();
                var b = context.Role.AddStringInArrayFilter(o => o.Name, list).ToList();

                Assert.AreEqual(a.Count, b.Count, UnitTestResource.TestAddStringInArrayFilter);
            }
        }

        [Test]
        public void TestAddStringEndsWithFilter()
        {
            using (var context = new CoreContext())
            {
                var a = context.User.Where(o => o.LoginName.EndsWith("a")).Expression.ToString();
                var b = context.User.AddStringEndsWithFilter("a", o => o.LoginName).Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddStringEndsWithFilter);
            }
        }

        [Test]
        public void TestAddDateTimeBetweenFilter()
        {
            using (var context = new CoreContext())
            {
                var list = context.User.OrderBy(o => o.CreateTime).Take(10).Select(o => o.CreateTime).ToList();
                var a = context.User.Where(o => o.CreateTime >= list.FirstOrDefault() && o.CreateTime <= list.LastOrDefault()).ToList();
                var b = context.User.AddDateTimeBetweenFilter(list.FirstOrDefault(), list.LastOrDefault(), o => o.CreateTime).ToList();

                Assert.AreEqual(a.Count, b.Count);
            }
        }

        [Test]
        public void TestAddIntegerBetweenFilter()
        {
            using (var context = new CoreContext())
            {
                var list = context.User.OrderBy(o => o.CreateTime).Take(10).Select(o => o.Id).ToList();
                var a = context.User.Where(o => o.Id >= list.FirstOrDefault() && o.Id <= list.LastOrDefault()).ToList();
                var b = context.User.AddIntegerBetweenFilter(list.FirstOrDefault(), list.LastOrDefault(), o => o.Id).ToList();

                Assert.AreEqual(a.Count, b.Count);
            }
        }

        [Test]
        public void TestAddStringStartsWithFilter()
        {
            using (var context = new CoreContext())
            {
                var a = context.User.Where(o => o.LoginName.StartsWith("a")).Expression.ToString();
                var b = context.User.AddStringStartsWithFilter("a", o => o.LoginName).Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddStringStartsWithFilter);
            }
        }

        [Test]
        public void TestAddStringEqualFilter()
        {
            using (var context = new CoreContext())
            {
                var a = context.User.Where(o => o.LoginName.Equals("a")).Expression.ToString();
                var b = context.User.AddStringEqualFilter("a", o => o.LoginName).Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddStringEqualFilter);
            }
        }

        [Test]
        public void TestAddDateTimeGreaterThanOrEqualFilters()
        {
            using (var context = new CoreContext())
            {
                var a = context.User.Where(o => o.CreateTime >= DateTime.Today).Expression.ToString().Replace("DateTime.Today", DateTime.Today.ToString());
                var b = context.User.AddDateTimeGreaterThanOrEqualFilter(DateTime.Today, o => o.CreateTime).Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddDateTimeGreaterThanOrEqualFilters);
            }
        }

        [Test]
        public void TestAddDateTimeLessThanOrEqualFilter()
        {
            using (var context = new CoreContext())
            {
                var a = context.User.Where(o => o.CreateTime <= DateTime.Today).Expression.ToString().Replace("DateTime.Today", DateTime.Today.ToString());
                var b = context.User.AddDateTimeLessThanOrEqualFilter(DateTime.Today, o => o.CreateTime).Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddDateTimeLessThanOrEqualFilter);
            }
        }

        [Test]
        public void TestAddStringNotNullFilter()
        {
            using (var context = new CoreContext())
            {
                var a = context.User.Where(o => o.LoginName != null).Expression.ToString();
                var b = context.User.AddStringNotNullFilter(o => o.LoginName).Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddStringEqualFilter);
            }
        }

        [Test]
        public void TestAddBooleanFilter()
        {
            using (var context = new CoreContext())
            {
                IQueryable<User> query = context.User;
                query = query.AddBooleanFilter(o => o.IsEnable, false);
                var a = context.User.Where(o => o.IsEnable == false).Expression.ToString();
                var b = query.Expression.ToString();

                Assert.AreEqual(a, b, UnitTestResource.TestAddBooleanFilter);
            }
        }
    }
}
