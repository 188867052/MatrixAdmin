using System;
using Microsoft.AspNetCore.Mvc;

namespace Core.Extension
{
    public class Url
    {
        public string Action { get; set; }

        public Type Type { get; set; }

        public Url(Type type, string action, string parameter = default)
        {
            this.Action = action;
            this.Type = type;
            this.Parameter = parameter;
        }

        public string Parameter { get; set; }

        public string Render()
        {
            string controller = Type.Name.Replace(nameof(Controller), default);
            return $"/{controller}/{this.Action}/{Parameter}";
        }
    }
}