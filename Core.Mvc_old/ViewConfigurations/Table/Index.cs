//using System.Collections.Generic;
//using Core.Web.Section;

//namespace Core.Mvc.ViewConfigurations.Table
//{
//	public class Index : IndexPage
//	{
//		public override IList<string> CreateCssFilePaths()
//		{
//			IList<string> list = new List<string>
//			{
//				"/css/bootstrap.css",
//				"/css/index.css",
//				"/css/Highlight/github.css"
//			};
//			return list;
//		}

//		protected override IList<string> CreateJavaScriptFilePaths()
//		{
//			IList<string> list = new List<string>
//			{
//				"/js/jquery-3.3.1.js",
//				"/js/bootstrap.js",
//				"/js/Vue.js",
//				"/js/index.js",
//				"/js/axios.js",
//				"/js/highlight.pack.js",
//				"/js/table.js",
//				"/js/Core.js",
//			};
//			return list;
//		}

//		public override Dictionary<string, string> JavascriptFields => new Dictionary<string, string>
//		{
//			{ "apiUrl", this.Url },
//			{ "bolMappingsTabId", TableIdentifiers.MappingTabId.Value },
//			{ "tableTabId", TableIdentifiers.TableTabId.Value }
//		};

//		public override string RenderBody()
//		{
//			Tab mappingTab = new Tab(TableIdentifiers.MappingTabId, "BOL Mapping", HTMLHelper.GenerateInputGroup("entityList", "entityInput", "onClickBOLMappings", "appendEntity"), true);
//			mappingTab.AddSection(new XmlSection("xml", "Entity"));
//			mappingTab.AddSection(new XmlSection("referencedXml", "Referenced"));

//			Tab tableTab = new Tab(TableIdentifiers.TableTabId, "Table", HTMLHelper.GenerateInputGroup("tableList", "tableInput", "onClickTables", "appendTables"));
//			tableTab.AddSection(new TableSection("tableColumn", "Columns"));
//			tableTab.AddSection(new TableSection("tableReferencedBy", "Referenced"));
//			tableTab.AddSection(new TableSection("triggers", "Triggers"));
//			tableTab.AddSection(new XmlSection("referencedEntityXml", "Referenced Entity"));

//			TabPage tabPage = new TabPage("Search");
//			tabPage.AddTab(mappingTab);
//			tabPage.AddTab(tableTab);

//			return tabPage.Render();
//		}
//	}
//}