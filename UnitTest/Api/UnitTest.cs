using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Api.Controllers;
using Core.Api.Routes;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NUnit.Framework;

namespace Core.UnitTest.Api
{
    /// <summary>
    /// Api unit test.
    /// </summary>
    [TestFixture]
    public class UnitTest
    {
        private AuthenticationHeaderValue authentication;

        [Test]
        [Order(1)]
        public async Task TestGetToken()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(SiteConfiguration.Host)
            };
            using (client)
            {
                HttpResponseMessage httpResponse = await client.GetAsync(LogRoute.Index);
                Task<string> json = httpResponse.Content.ReadAsStringAsync();
            }
            var data = await HttpClientAsync.Async(AuthenticationRoute.Auth, new { username = "admin", password = "111111" });

            Console.WriteLine(data);
            int code = data.code;
            this.authentication = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, data.token.ToString());

            Assert.AreEqual(code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestAuthentication()
        {
            dynamic data = await HttpClientAsync.Async(TestRoute.TestAuthentication, this.authentication);
            Console.WriteLine(data);
            bool isAuthenticated = data.isAuthenticated;

            Assert.IsTrue(isAuthenticated);
        }

        [Test]
        public async Task TestUnAuthenticate()
        {
            var url = new Url(typeof(TestController), nameof(TestController.TestAuthentication));
            using (HttpClient client = HttpClientAsync.CreateInstance())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);
            }
        }

        [Test]
        public async Task TestGetUserDataListAsync()
        {
            ResponseModel model = await HttpClientAsync.Async<IList<UserModel>>(DataListRoute.GetUserDataList);
            IList<UserModel> users = (IList<UserModel>)model.Data;

            Assert.GreaterOrEqual(users.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK, UnitTestResource.TestGetUserDataList);
        }

        [Test]
        public async Task TestGetRoleDataListAsync()
        {
            ResponseModel model = await HttpClientAsync.Async<IList<RoleModel>>(DataListRoute.GetRoleDataList);
            IList<RoleModel> roles = (IList<RoleModel>)model.Data;

            Assert.GreaterOrEqual(roles.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK, UnitTestResource.TestGetRoleDataList);
        }

        [Test]
        public async Task TestGetMenuDataListAsync()
        {
            ResponseModel model = await HttpClientAsync.Async<IList<MenuModel>>(DataListRoute.GetMenuDataList);
            IList<MenuModel> menus = (IList<MenuModel>)model.Data;

            Assert.GreaterOrEqual(menus.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK, UnitTestResource.TestGetMenuDataList);
        }

        [Test]
        public async Task TestUserIndexAsync()
        {
            ResponseModel model = await HttpClientAsync.Async<IList<UserModel>>(UserRoute.Index, this.authentication);
            IList<UserModel> menus = (IList<UserModel>)model.Data;

            Assert.GreaterOrEqual(menus.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestRoleIndexAsync()
        {
            ResponseModel model = await HttpClientAsync.Async<IList<RoleModel>>(RoleRoute.Index);
            IList<RoleModel> menus = (IList<RoleModel>)model.Data;

            Assert.GreaterOrEqual(menus.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestMenuIndexAsync()
        {
            ResponseModel model = await HttpClientAsync.Async<IList<MenuModel>>(MenuRoute.Index);
            IList<MenuModel> menus = (IList<MenuModel>)model.Data;

            Assert.GreaterOrEqual(menus.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestLogIndexAsync()
        {
            ResponseModel model = await HttpClientAsync.Async<IList<LogModel>>(LogRoute.Index);
            IList<LogModel> menus = (IList<LogModel>)model.Data;

            Assert.GreaterOrEqual(menus.Count, 0);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestUserFindByIdAsync()
        {
            ResponseModel model = await HttpClientAsync.Async<UserModel>(UserRoute.FindById, 1);
            UserModel user = (UserModel)model.Data;

            Assert.IsNotNull(user);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestMenuFindByIdAsync()
        {
            ResponseModel model = await HttpClientAsync.Async<MenuModel>(MenuRoute.FindById, 1);
            MenuModel user = (MenuModel)model.Data;

            Assert.IsNotNull(user);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestRoleFindByIdAsync()
        {
            ResponseModel model = await HttpClientAsync.Async<RoleModel>(RoleRoute.FindById, 1);
            RoleModel user = (RoleModel)model.Data;

            Assert.IsNotNull(user);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Test enable user.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestEnableUserAsync()
        {
            User user = DapperExtension.Connection.QueryFirst<User>(o => o.IsEnable, true);
            if (user != null)
            {
                dynamic model = await HttpClientAsync.Async(UserRoute.Disable, user.Id);
                int code = model.code;
                Assert.AreEqual(code, (int)HttpStatusCode.OK);
                user = DapperExtension.Connection.QueryFirst<User>(o => o.Id, user.Id);
                Assert.IsFalse(user.IsEnable, UnitTestResource.DisableFail);

                model = await HttpClientAsync.Async(UserRoute.Enable, user.Id);
                code = model.code;
                Assert.AreEqual(code, (int)HttpStatusCode.OK);
                user = DapperExtension.Connection.QueryFirst<User>(o => o.Id, user.Id);
                bool isEnable = user.IsEnable;
                Assert.IsTrue(isEnable, UnitTestResource.EnableFail);
            }
        }
    }
}
