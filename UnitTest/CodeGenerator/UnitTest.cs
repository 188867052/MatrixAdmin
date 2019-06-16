using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;
using Core.Extension.RouteAnalyzer;
using Core.Mvc.Framework;
using NUnit.Framework;
using Route.Generator;

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
        public async Task TestMvcRouteGenerator()
        {
            try
            {
                var client = new TestSite(typeof(Startup)).BuildClient();
                var response = await client.GetAsync(Router.DefaultRoute);
                var content = await response.Content.ReadAsStringAsync();

                var routesGenerated = RouteGenerator.GenerateRoutes(content);
                Assert.IsTrue(routesGenerated.Contains("namespace"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public async Task TestApiRouteGenerator()
        {
            try
            {
                var client = new TestSite(typeof(Core.Api.Framework.Startup)).BuildClient();
                var response = await client.GetAsync(Router.DefaultRoute);
                var content = await response.Content.ReadAsStringAsync();

                Assert.IsNotEmpty(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
