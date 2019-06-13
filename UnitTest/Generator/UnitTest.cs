using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Entity;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Core.UnitTest.CodeGenerator
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void TestDbContextHasSameProperty()
        {
            var standardProperties = typeof(CoreContext).GetProperties().OrderBy(o => o.Name);
            var properties = typeof(Data.CoreContext).GetProperties().OrderBy(o => o.Name);

            Assert.AreEqual(string.Join(",", standardProperties.Select(p => p.PropertyType.Name)), string.Join(",", properties.Select(p => p.PropertyType.Name)));
            Assert.AreEqual(string.Join(",", standardProperties.Select(p => p.Name)), string.Join(",", properties.Select(p => p.Name)));
        }

        [Test]
        public void TestEntitiesHasSameProperty()
        {
            var standardProperties = typeof(CoreContext).GetProperties().OrderBy(o => o.Name);
            var properties = typeof(Data.CoreContext).GetProperties().OrderBy(o => o.Name);

            foreach (var item in standardProperties)
            {
                var entityType = properties.FirstOrDefault(o => o.PropertyType.Name == item.PropertyType.Name && o.Name == item.Name);
                Assert.IsNotNull(entityType);
                var standardEntityProperties = item.PropertyType.GetProperties().OrderBy(o => o.Name);
                var entityProperties = entityType.PropertyType.GetProperties().OrderBy(o => o.Name);

                Assert.AreEqual(string.Join(",", standardEntityProperties.Select(p => p.PropertyType.Name)), string.Join(",", standardEntityProperties.Select(p => p.PropertyType.Name)));
                Assert.AreEqual(string.Join(",", entityProperties.Select(p => p.Name)), string.Join(",", entityProperties.Select(p => p.Name)));
            }
        }

        [Test]
        public async Task TestRouteGenerator()
        {
            try
            {
                var client = new TestSite(typeof(Core.Api.Framework.StartupConfigurations.Startup)).BuildClient();
                var response = await client.GetAsync("/swagger/v1/swagger.json");
                var content = await response.Content.ReadAsStringAsync();

                //Console.WriteLine(content);
                Assert.NotNull(content);

                this.Generate(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public async Task TestRouteGenerator2()
        {
            try
            {
                var client = new TestSite(typeof(Core.Api.Framework.StartupConfigurations.Startup)).BuildClient();
                var response = await client.GetAsync("/routes");
                var content = await response.Content.ReadAsStringAsync();

                var response2 = await client.GetAsync("/swagger/v1/swagger.json");
                var content2 = await response2.Content.ReadAsStringAsync();

                var response3 = await client.GetAsync("/swagger/index.html");
                var content3 = await response3.Content.ReadAsStringAsync();


                using (HttpClient HttpClient = new HttpClient())
                {
                    var httpResponse = await HttpClient.GetAsync("http://localhost:54321/routes");
                    var content4 = await httpResponse.Content.ReadAsStringAsync();
                }

                Assert.NotNull(content);

                this.Generate(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Generate(string content)
        {
            var a = JsonConvert.DeserializeObject<dynamic>(content);
            IEnumerable<dynamic> b = a.paths;

            IList<Route> list = new List<Route>();
            foreach (dynamic item in b)
            {
                Route route = new Route();
                var first = item.First;
                this.TryGet(first, "get", out dynamic get);
                if (get != null)
                {
                    route.HttpMethod = "get";
                    route.ControllerName = get.tags[0];
                    route.Parameters = get.parameters.ToString();
                    route.Path = item.Name;
                }
                else
                {
                    this.TryGet(first, "post", out dynamic post);
                    if (post != null)
                    {
                        route.HttpMethod = "post";
                        route.ControllerName = post.tags[0];
                        route.Parameters = post.requestBody.content.ToString();
                        route.Path = item.Name;
                    }
                }

                list.Add(route);
                Console.WriteLine(JsonConvert.SerializeObject(route));
            }
        }

        private bool TryGet(dynamic dynamicIn, string key, out dynamic dynamicOut)
        {
            try
            {
                dynamicOut = dynamicIn[key];
                return true;
            }
            catch (Exception ex)
            {
                dynamicOut = null;
                return false;
            }
        }
    }

    public class Route
    {
        public string HttpMethod { get; set; }
        public string Path { get; set; }
        public string ControllerName { get; set; }
        public string Parameters { get; internal set; }
    }
}
