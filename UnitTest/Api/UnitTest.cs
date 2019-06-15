//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;
//using Core.Api.Controllers;
//using Core.Api.Routes;
//using Core.Entity;
//using Core.Extension;
//using Core.Extension.Dapper;
//using Core.Model;
//using Core.Model.Administration.Menu;
//using Core.Model.Administration.Role;
//using Core.Model.Administration.User;
//using Core.Model.Log;
//using Core.Mvc.Framework;
//using Core.UnitTest.Resource.Areas;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using NUnit.Framework;

//namespace Core.UnitTest.Api
//{
//    /// <summary>
//    /// Api unit test.
//    /// </summary>
//    [TestFixture]
//    public class UnitTest
//    {
//        private AuthenticationHeaderValue authentication;

//        [Test]
//        [Order(1)]
//        public async Task TestGetToken()
//        {
//            var url = new Url(typeof(AuthenticationController), nameof(AuthenticationController.Auth));
//            var data = await HttpClientAsync.GetAsync(url, parameters: new { username = "admin", password = "111111" });

//            Console.WriteLine(data);
//            int code = data.code;
//            this.authentication = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, data.token.ToString());

//            Assert.AreEqual(code, (int)HttpStatusCode.OK);
//        }

//        [Test]
//        public async Task TestAuthentication()
//        {
//            var url = new Url(typeof(TestController), nameof(TestController.TestAuthentication));
//            dynamic data = await HttpClientAsync.GetAsync(url, this.authentication);
//            Console.WriteLine(data);
//            bool isAuthenticated = data.isAuthenticated;

//            Assert.IsTrue(isAuthenticated);
//        }

//        [Test]
//        public async Task TestUnAuthenticate()
//        {
//            var url = new Url(typeof(TestController), nameof(TestController.TestAuthentication));
//            HttpResponseMessage response = await HttpClientAsync.GetResponseAsync(url);

//            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);
//        }

//        [Test]
//        public async Task TestGetUserDataListAsync()
//        {
//            var url = new Url(typeof(DataListController), nameof(DataListController.GetUserDataList));
//            ResponseModel model = await HttpClientAsync.GetAsync<IList<UserModel>>(url);
//            IList<UserModel> users = (IList<UserModel>)model.Data;

//            Assert.GreaterOrEqual(users.Count, 0);
//            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK, UnitTestResource.TestGetUserDataList);
//        }

//        [Test]
//        public async Task TestGetRoleDataListAsync()
//        {
//            var url = new Url(typeof(DataListController), nameof(DataListController.GetRoleDataList));
//            ResponseModel model = await HttpClientAsync.GetAsync<IList<RoleModel>>(url);
//            IList<RoleModel> roles = (IList<RoleModel>)model.Data;

//            Assert.GreaterOrEqual(roles.Count, 0);
//            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK, UnitTestResource.TestGetRoleDataList);
//        }

//        [Test]
//        public async Task TestGetMenuDataListAsync()
//        {
//            var url = new Url(typeof(DataListController), nameof(DataListController.GetMenuDataList));
//            ResponseModel model = await HttpClientAsync.GetAsync<IList<MenuModel>>(url);
//            IList<MenuModel> menus = (IList<MenuModel>)model.Data;

//            Assert.GreaterOrEqual(menus.Count, 0);
//            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK, UnitTestResource.TestGetMenuDataList);
//        }

//        [Test]
//        public async Task TestUserIndexAsync()
//        {
//            ResponseModel model = await HttpClientAsync.GetAsync<IList<UserModel>>(UserRoute.Index, this.authentication);
//            IList<UserModel> menus = (IList<UserModel>)model.Data;

//            Assert.GreaterOrEqual(menus.Count, 0);
//            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
//        }

//        [Test]
//        public async Task TestRoleIndexAsync()
//        {
//            ResponseModel model = await HttpClientAsync.GetAsync<IList<RoleModel>>(RoleRoute.Index);
//            IList<RoleModel> menus = (IList<RoleModel>)model.Data;

//            Assert.GreaterOrEqual(menus.Count, 0);
//            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
//        }

//        [Test]
//        public async Task TestMenuIndexAsync()
//        {
//            ResponseModel model = await HttpClientAsync.GetAsync<IList<MenuModel>>(MenuRoute.Index);
//            IList<MenuModel> menus = (IList<MenuModel>)model.Data;

//            Assert.GreaterOrEqual(menus.Count, 0);
//            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
//        }

//        [Test]
//        public async Task TestLogIndexAsync()
//        {
//            ResponseModel model = await HttpClientAsync.GetAsync<IList<LogModel>>(LogRoute.Index);
//            IList<LogModel> menus = (IList<LogModel>)model.Data;

//            Assert.GreaterOrEqual(menus.Count, 0);
//            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
//        }

//        [Test]
//        public async Task TestUserFindByIdAsync()
//        {
//            ResponseModel model = await HttpClientAsync.GetAsync<UserModel>(UserRoute.FindById, 1);
//            UserModel user = (UserModel)model.Data;

//            Assert.IsNotNull(user);
//            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
//        }

//        [Test]
//        public async Task TestMenuFindByIdAsync()
//        {
//            ResponseModel model = await HttpClientAsync.GetAsync<MenuModel>(MenuRoute.FindById, 1);
//            MenuModel user = (MenuModel)model.Data;

//            Assert.IsNotNull(user);
//            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
//        }

//        [Test]
//        public async Task TestRoleFindByIdAsync()
//        {
//            ResponseModel model = await HttpClientAsync.GetAsync<RoleModel>(RoleRoute.FindById, 1);
//            RoleModel user = (RoleModel)model.Data;

//            Assert.IsNotNull(user);
//            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
//        }

//        /// <summary>
//        /// Test enable user.
//        /// </summary>
//        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
//        [Test]
//        public async Task TestEnableUserAsync()
//        {
//            User user = DapperExtension.Connection.QueryFirst<User>(o => o.IsEnable, true);
//            if (user != null)
//            {
//                var url = new Url(typeof(UserController), nameof(UserController.Disable));
//                ResponseModel model = await HttpClientAsync.DeleteAsync(url, user.Id);
//                Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
//                user = DapperExtension.Connection.QueryFirst<User>(o => o.Id, user.Id);
//                Assert.IsFalse(user.IsEnable, UnitTestResource.DisableFail);

//                url = new Url(typeof(UserController), nameof(UserController.Enable));
//                model = await HttpClientAsync.DeleteAsync(url, user.Id);
//                Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
//                user = DapperExtension.Connection.QueryFirst<User>(o => o.Id, user.Id);
//                Assert.IsTrue(user.IsEnable, UnitTestResource.EnableFail);
//            }
//        }
//    }
//}
