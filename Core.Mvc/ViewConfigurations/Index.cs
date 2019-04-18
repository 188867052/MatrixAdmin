using Core.Web;
using Core.Web.Html;
using Core.Web.JavaScript;
using System.Collections.Generic;
using Core.Repository;
using Attribute = Core.Web.Attribute;

namespace Core.Mvc.ViewConfigurations
{
	public abstract class IndexPage
	{
		public string ClassName
		{
			get
			{
				{
					return Controller;
				}
			}
		}

		public void SetRoute(string controller, string action)
		{
			this.Action = action;
			this.Controller = controller;
			this.Url = $"{new Config().Url}/api/{controller}/";
		}

		protected string Url { get; set; }

		private string Controller { get; set; }

		private string Action { get; set; }

		public abstract IList<string> CreateCssFilePaths();

		protected abstract IList<string> CreateJavaScriptFilePaths();

		public string RenderHeader()
		{
			string renderFile = this.GenerateCssHref() + this.GenerateScriptSource();
			Node node = new Node(TagName.script, this.Initialize());
			Node headNode = new Node(TagName.head);
			headNode.AddChildNode(new Node(TagName.title, "Search"));
			headNode.AddChildNode(node);
			return renderFile + headNode.Render();
		}

		private string GenerateCssHref()
		{
			string cssHref = string.Empty;
			foreach (string css in CreateCssFilePaths())
			{
				Dictionary<Attribute, string> dictionary = new Dictionary<Attribute, string>
				{
					{Attribute.href, css}, {Attribute.rel, AttributeValue.stylesheet.ToString()}
				};
				Node node = new Node(TagName.link, dictionary);
				cssHref += node.Render();
			};

			return cssHref;
		}

		private string GenerateScriptSource()
		{
			string javascript = string.Empty;
			foreach (string item in CreateJavaScriptFilePaths())
			{
				Dictionary<Attribute, string> dictionary = new Dictionary<Attribute, string>
				{
					{Attribute.src, item}
				};
				Node node = new Node(TagName.script, dictionary);
				javascript += node.Render();
			}

			return javascript;
		}

		private string Initialize()
		{
			JavaScript javaScript = new JavaScript(this.FirstToLower(this.ClassName), this.ClassName)
			{
				Fields = this.JavascriptFields
			};

			return javaScript.Render();
		}

		private string FirstToLower(string str)
		{
			string returnString = string.Empty;
			for (int i = 0; i < str.ToCharArray().Length; i++)
			{
				returnString += i == 0 ? str[i].ToString().ToLower() : str[i].ToString();
			}
			return returnString;
		}

		public abstract Dictionary<string, string> JavascriptFields { get; }

		public abstract string RenderBody();
	}
}