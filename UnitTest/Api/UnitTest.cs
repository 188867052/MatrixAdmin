using System.Collections.Generic;
using System.Linq;
using System.Net;
using Core.Api.Controllers;
using Core.Entity;
using Core.Entity.Enums;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Model.Administration.Role;
using Core.Model.Administration.User;
using Core.Model.Log;
using Core.Mvc.Framework;
using Core.UnitTest.Resource.Areas;
using Dapper;
using NUnit.Framework;

namespace Core.UnitTest.Api
{
    [TestFixture]
    public class UnitTest
    {
        private readonly CoreApiContext _coreApiContext = new CoreApiContext();

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
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK, UnitTestResource.TestGetMenuDataList);
        }

        [Test]
        public void TestUserIndex()
        {
            var url = new Url(typeof(UserController), nameof(UserController.Index));
            ResponseModel model = HttpClientAsync.GetAsync<IList<UserModel>>(url).Result;
            IList<UserModel> menus = (IList<UserModel>)model.Data;

            Assert.GreaterOrEqual(menus.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public void TestRoleIndex()
        {
            var url = new Url(typeof(RoleController), nameof(RoleController.Index));
            ResponseModel model = HttpClientAsync.GetAsync<IList<RoleModel>>(url).Result;
            IList<RoleModel> menus = (IList<RoleModel>)model.Data;

            Assert.GreaterOrEqual(menus.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public void TestMenuIndex()
        {
            var url = new Url(typeof(MenuController), nameof(MenuController.Index));
            ResponseModel model = HttpClientAsync.GetAsync<IList<MenuModel>>(url).Result;
            IList<MenuModel> menus = (IList<MenuModel>)model.Data;

            Assert.GreaterOrEqual(menus.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public void TestLogIndex()
        {
            var url = new Url(typeof(LogController), nameof(LogController.Index));
            ResponseModel model = HttpClientAsync.GetAsync<IList<LogModel>>(url).Result;
            IList<LogModel> menus = (IList<LogModel>)model.Data;

            Assert.GreaterOrEqual(menus.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public void TestUserFindById()
        {
            var url = new Url(typeof(UserController), nameof(UserController.FindById));
            ResponseModel model = HttpClientAsync.GetAsync<UserModel>(url, 1).Result;
            UserModel user = (UserModel)model.Data;

            Assert.IsNotNull(user);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public void TestMenuFindById()
        {
            var url = new Url(typeof(MenuController), nameof(MenuController.FindById));
            ResponseModel model = HttpClientAsync.GetAsync<MenuModel>(url, 1).Result;
            MenuModel user = (MenuModel)model.Data;

            Assert.IsNotNull(user);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public void TestRoleFindById()
        {
            var url = new Url(typeof(RoleController), nameof(RoleController.FindById));
            ResponseModel model = HttpClientAsync.GetAsync<RoleModel>(url, 1).Result;
            RoleModel user = (RoleModel)model.Data;

            Assert.IsNotNull(user);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public void TestEnableUser()
        {
            User user = _coreApiContext.User.FirstOrDefault(o => o.IsEnable);
            if (user != null)
            {
                var url = new Url(typeof(UserController), nameof(UserController.DisEnable));
                ResponseModel model = HttpClientAsync.DeleteAsync(url, user.Id).Result;

                user = CoreApiContext.Dapper.QueryFirstOrDefault<User>("SELECT * FROM [User] WHERE Id = @Id", new { Id = user.Id });
                Assert.IsFalse(user.IsEnable, "禁用用户失败");

                url = new Url(typeof(UserController), nameof(UserController.Enable));
                model = HttpClientAsync.DeleteAsync(url, user.Id).Result;
                Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
                user = CoreApiContext.Dapper.QueryFirstOrDefault<User>("SELECT * FROM [User] WHERE Id = @Id", new { Id = user.Id });
                Assert.IsTrue(user.IsEnable, "启用用户失败");
            }
        }
    }
}
