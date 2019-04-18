using System.Collections.Generic;

namespace Core.Web.Html
{
	public class Node
	{
		public Node(TagName name, Dictionary<Attribute, string> dictionary)
		{
			this.Name = name;
			this.Dictionary = dictionary;
		}

		public Node(TagName name)
		{
			this.Name = name;
		}

		public void AddChildNode(Node node)
		{
			if (this.ChildNodes == null)
			{
				this.ChildNodes = new List<Node>();
			}
			this.ChildNodes.Add(node);
		}

		public Node(TagName name, Attribute key, string attributeValue, string value)
		{
			this.Name = name;
			this.Dictionary = new Dictionary<Attribute, string> { { key, attributeValue } };
			this.Value = value;
		}

		public Node(TagName name, Attribute key, string attributeValue)
		{
			this.Name = name;
			this.Dictionary = new Dictionary<Attribute, string> { { key, attributeValue } };
		}

		public Node(TagName name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		public Node(TagName name, Dictionary<Attribute, string> dictionary, string value)
		{
			this.Name = name;
			this.Dictionary = dictionary;
			this.Value = value;
		}

		Dictionary<Attribute, string> Dictionary { get; }

		private IList<Node> ChildNodes { get; set; }

		private TagName Name { get; }

		private string Value { get; set; }

		public string Render()
		{
			string attributeText = string.Empty;
			if (this.Dictionary != null)
			{
				foreach (KeyValuePair<Attribute, string> keyValuePair in this.Dictionary)
				{
					attributeText += this.SetAttribute(keyValuePair.Key.ToString().Replace("_", "-"), keyValuePair.Value);
				}
			}
			if (this.ChildNodes != null)
			{
				foreach (var node in ChildNodes)
				{
					this.Value += node.Render();
				}
			}
			return $"<{Name} {attributeText}>{Value}</{Name}>";
		}

		private string SetAttribute(string name, string value)
		{
			return $"{name}=\"{value}\"";
		}
	}
}