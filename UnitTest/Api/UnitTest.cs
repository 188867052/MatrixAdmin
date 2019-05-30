using System.Collections.Generic;
using System.Net;
using Core.Api.Controllers;
using Core.Entity;
using Core.Extension;
using Core.Extension.Dapper;
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
    /// <summary>
    /// Api unit test.
    /// </summary>
    [TestFixture]
    public class UnitTest
    {
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

        /// <summary>
        /// Test enable user.
        /// </summary>
        [Test]
        public void TestEnableUser()
        {
            User user = DapperExtension.Connection.QueryFirst<User>("SELECT * FROM [User] WHERE is_enable = @IsEnable", new { IsEnable = true });
            if (user != null)
            {
                var url = new Url(typeof(UserController), nameof(UserController.DisEnable));
                ResponseModel model = HttpClientAsync.DeleteAsync(url, user.Id).Result;
                Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
                user = DapperExtension.Connection.QueryFirst<User>("SELECT * FROM [User] WHERE id = @id", new { user.Id });
                Assert.IsFalse(user.IsEnable, UnitTestResource.DisEnableFail);

                url = new Url(typeof(UserController), nameof(UserController.Enable));
                model = HttpClientAsync.DeleteAsync(url, user.Id).Result;
                Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
                user = DapperExtension.Connection.QueryFirst<User>("SELECT * FROM [User] WHERE id = @id", new { user.Id });
                Assert.IsTrue(user.IsEnable, UnitTestResource.EnableFail);
            }
        }
    }
}
