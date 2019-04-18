using System.Collections.Generic;
using System.Text;

namespace Core.Web.JavaScript
{
	/// <summary>
	/// JavaScript used to initialize a view instance.
	/// </summary>
	public class JavaScript
	{
		public JavaScript(string globalVariableName, string className)
		{
			this.GlobalVariableName = globalVariableName;
			this.ClassName = className;
			this.Fields = new Dictionary<string, string>();
		}

		public Dictionary<string, string> Fields { get; set; }

		public string GlobalVariableName { get; set; }

		public string ClassName { get; set; }

		public string Render()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in this.Fields)
			{
				stringBuilder.Append($"{this.GlobalVariableName}._{keyValuePair.Key}=\"{keyValuePair.Value}\";");
			}

			string variableName = "Core";
			string url = "";
			//stringBuilder.Append($"{variableName}._{url}=\"{keyValuePair.Value}\";");

			return $"window.{this.GlobalVariableName} = new {this.ClassName}();{stringBuilder}";
		}
	}
}