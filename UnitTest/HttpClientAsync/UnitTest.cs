using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Core.Api.Framework;
using Core.Api.Routes;
using Core.Entity;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Model.Administration.User;
using Core.Model.Log;
using Newtonsoft.Json;
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
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestGetWithNoParameterGeneratedAsync()
        {
            var model = await PermissionRoute.IndexAsync<HttpResponseModel>();
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestGetWithOneAttributeParameterAsync()
        {
            var model = await HttpClientAsync.Async<MenuModel>(MenuRoute.FindById, 1);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestGetWithOneAttributeParameterGeneratedAsync()
        {
            var model = await MenuRoute.FindByIdAsync<HttpResponseModel>(1);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestGetWithOneParameterAsync()
        {
            var model = await HttpClientAsync.Async<UserModel>(UserRoute.FindById, 1);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestGetWithOneParameterGeneratedAsync()
        {
            var model = await UserRoute.FindByIdAsync<HttpResponseModel>(1);
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestGetWithMultipleParameterAsync()
        {
            dynamic model = await HttpClientAsync.Async(AuthenticationRoute.Auth, "admin", "111111");
            int code = model.code;
            Assert.AreEqual(code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestGetWithMultipleParameterGeneratedAsync()
        {
            var model = await AuthenticationRoute.AuthAsync<dynamic>("admin", "111111");
            int code = model.code;
            Assert.AreEqual(code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestPostWithParameterAsync()
        {
            HttpResponseModel model = await HttpClientAsync.Async<IList<LogModel>>(LogRoute.Search, new LogPostModel() { PageSize = 10, PageIndex = 1 });
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);
        }

        [Test]
        public async Task TestPostWithParameterGeneratedAsync()
        {
            HttpResponseModel model = await LogRoute.SearchAsync<HttpResponseModel>(new LogPostModel() { PageSize = 10, PageIndex = 1 });
            Assert.AreEqual(model.Code, (int)HttpStatusCode.OK);

            IList<LogModel> data = JsonConvert.DeserializeObject<IList<LogModel>>(model.Data.ToString());
            Assert.NotNull(data);
        }
    }
}
