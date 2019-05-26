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
using Core.UnitTest.Resource.Areas;
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
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK, UnitTestResource.TestAddBooleanFilter);
        }
    }
}
