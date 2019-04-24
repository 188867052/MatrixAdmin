using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.Web.Button
{
    public class Button
    {
        public Button(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        public string Render()
        {
            return $"<button type=\"submit\" class=\"btn btn-primary\">{Text}</button>" + Environment.NewLine;
        }
    }
}
