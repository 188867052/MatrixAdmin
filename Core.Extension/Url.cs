using System;
using Microsoft.AspNetCore.Mvc;

namespace Core.Extension
{
    public class Url
    {
        public string Action { get; set; }

        public Type Type { get; set; }

        public Url(Type type, string action)
        {
            this.Action = action;
            this.Type = type;
        }

        public string Render()
        {
            string controller = Type.Name.Replace(nameof(Controller), default);
            return $"/{controller}/{this.Action}";
        }
    }
}