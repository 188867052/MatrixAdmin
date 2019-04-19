using Core.Web;
using System.Collections.Generic;
using System.Text;
using Core.Web.Html;

namespace Core.Mvc
{
	public class TabPage
	{
		public TabPage(string pageTitle)
		{
			this.PageTitle = pageTitle;
			this.Tabs = new List<Tab>();
		}

		public string PageTitle { get; set; }

		public void AddTab(Tab tab)
		{
			this.Tabs.Add(tab);
		}

		private string RenderButtons()
		{
			Dictionary<Attribute, string> buttonContainer = new Dictionary<Attribute, string>
			{
				{ Attribute.@class, "btn-group btn-group-lg btn-group-justified" },
				{ Attribute.style, "width: 30%" },
				{ Attribute.role, "tablist" },
			};
			Node node = new Node(TagName.div, buttonContainer);
			foreach (Tab tab in Tabs)
			{
				Dictionary<Attribute, string> button = new Dictionary<Attribute, string>
				{
					{ Attribute.data_toggle, "tab" },
					{ Attribute.role, "tab" },
					{ Attribute.@class, "btn btn-default" },
					{ Attribute.href, $"#{tab.Id.Value}" },
				};
				node.AddChildNode(new Node(TagName.a, button, tab.Name));
			}
			return node.Render();
		}

		private IList<Tab> Tabs { get; }

		public string Render()
		{
			StringBuilder tabSections = new StringBuilder();
			string tabButtons = this.RenderButtons();
			foreach (Tab tab in Tabs)
			{
				tabSections.Append(tab.RenderSections());
			}

			Node tabContent = new Node(TagName.div, Attribute.@class, "tab-content", tabSections.ToString());
			Dictionary<Attribute, string> dictionary = new Dictionary<Attribute, string>
			{
				{ Attribute.id, "app" },
				{ Attribute.style, "padding: 10px"},
			};
			Node node = new Node(TagName.body);
			node.AddChildNode(new Node(TagName.div, dictionary, tabButtons + tabContent.Render()));
			return node.Render();
		}
	}
}