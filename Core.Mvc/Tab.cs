using Core.Web.Html;
using Core.Web.Identifiers;
using System.Collections.Generic;
using System.Text;
using Attribute = Core.Web.Attribute;

namespace Core.Mvc
{
	public class Tab
	{
		public Tab(Identifier id, string name, string searchInput, bool isSelected = false)
		{
			this.Id = id;
			this.Name = name;
			this.SearchInput = searchInput;
			this.Sections = new List<IRender>();
			this.IsSelected = isSelected;
		}

		public void AddSection(IRender section)
		{
			this.Sections.Add(section);
		}

		public Identifier Id { get; }

		public string Name { get; set; }

		public string SearchInput { get; }

		private IList<IRender> Sections { get; }

		public bool IsSelected { get; }

		public string RenderSections()
		{
			string fadeInClass = this.IsSelected ? "fade in active tab-pane" : "fade tab-pane";
			Dictionary<Attribute, string> tabAttributes = new Dictionary<Attribute, string>
			{
				{ Attribute.role, "tabpanel" },
				{ Attribute.@class, fadeInClass},
				{ Attribute.id,this.Id.Value},
			};
			StringBuilder stringBuilder = new StringBuilder();
			foreach (IRender section in this.Sections)
			{
				stringBuilder.Append(section.Render());
			}
			Node panel = new Node(TagName.div, tabAttributes, this.SearchInput + "<p></p>" + stringBuilder);
			return panel.Render();
		}
	}
}
