using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Core.Api.Controllers;
using Core.Entity;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Model.Administration.Role;
using Core.Model.Administration.User;
using Core.Mvc.Framework;
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
            var a = _coreApiContext.User.Where(o => new[] { 1, 2, 3 }.Contains(o.Id)).ToList();
            var b = _coreApiContext.User.AddIntegerInArrayFilter(o => o.Id, new[] { 1, 2, 3 }).ToList();

            Assert.AreEqual(a.Count, b.Count, UnitTestResource.TestAddIntegerInArrayFilter);
        }

        [Test]
        public void TestAddStringInArrayFilter()
        {
            var a = _coreApiContext.Role.Where(o => new[] { "超级管理员", "普通用户", "管理员" }.Contains(o.Name)).ToList();
            var b = _coreApiContext.Role.AddStringInArrayFilter(o => o.Name, new[] { "超级管理员", "普通用户", "管理员" }).ToList();

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

        [Test]
        public void TestGetUserDataList()
        {
            var url = new Url(typeof(DataListController), nameof(DataListController.GetUserDataList));
            ResponseModel model = HttpClientAsync.GetAsync<IList<UserModel>>(url).Result;
            IList<UserModel> users = (IList<UserModel>)model.Data;

            Assert.GreaterOrEqual(users.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK, UnitTestResource.TestGetUserDataList);
        }

        [Test]
        public void TestGetRoleDataList()
        {
            var url = new Url(typeof(DataListController), nameof(DataListController.GetRoleDataList));
            ResponseModel model = HttpClientAsync.GetAsync<IList<RoleModel>>(url).Result;
            IList<RoleModel> roles = (IList<RoleModel>)model.Data;

            Assert.GreaterOrEqual(roles.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK, UnitTestResource.TestGetRoleDataList);
        }

        [Test]
        public void TestGetMenuDataList()
        {
            var url = new Url(typeof(DataListController), nameof(DataListController.GetMenuDataList));
            ResponseModel model = HttpClientAsync.GetAsync<IList<MenuModel>>(url).Result;
            IList<MenuModel> menus = (IList<MenuModel>)model.Data;

            Assert.GreaterOrEqual(menus.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK, UnitTestResource.TestAddBooleanFilter);
        }
    }
}
