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

        public Url(string area, Type type, string action, string parameter = default) : this(type, action, parameter)
        {
            this.Area = area;
        }

        public string Area { get; set; }


        public string Parameter { get; set; }

        public string Render()
        {
            string controller = this.Type.Name.Replace(nameof(Controller), default);
            string url = default;
            if (this.Area != default)
            {
                url += $"/{this.Area}";
            }

            string parameter = default;
            if (this.Parameter != default)
            {
                parameter += $"/{this.Parameter}";
            }

            return $"{url}/{controller}/{this.Action}{parameter}";
        }
    }
}