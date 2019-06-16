using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Api.Routes;
using Core.Entity;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Model.Administration.User;
using Core.Model.Log;
using Core.Mvc.Framework;
using NUnit.Framework;

namespace Core.UnitTest.Async
{
    /// <summary>
    /// HttpClientAsync unit test.
    /// </summary>
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public async Task TestGetWithNoParameterAsync()
        {
            var model = await HttpClientAsync.Async<IList<Permission>>(PermissionRoute.Index);
            Assert.AreEqual(model.Code, 200);
        }

        [Test]
        public async Task TestGetWithOneAttributeParameterAsync()
        {
            var model = await HttpClientAsync.Async<MenuModel>(MenuRoute.FindById, 1);
            Assert.AreEqual(model.Code, 200);
        }

        [Test]
        public async Task TestGetWithOneParameterAsync()
        {
            var model = await HttpClientAsync.Async<UserModel>(UserRoute.FindById, 1);
            Assert.AreEqual(model.Code, 200);
        }

        [Test]
        public async Task TestGetWithMultipleParameterAsync()
        {
            dynamic model = await HttpClientAsync.Async(AuthenticationRoute.Auth, "admin", "111111");
            int code = model.code;
            Assert.AreEqual(code, 200);
        }

        [Test]
        public async Task TestPostWithParameterAsync()
        {
            ResponseModel model = await HttpClientAsync.Async<IList<LogModel>>(LogRoute.Search, new LogPostModel() { PageSize = 10, PageIndex = 1 });
            Assert.AreEqual(model.Code, 200);
        }
    }
}
