using System;
using Microsoft.AspNetCore.Mvc;

namespace Core.Extension
{
    public class Url
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Url"/> class.
        /// </summary>
        /// <param name="controllerType"></param>
        /// <param name="action"></param>
        /// <param name="parameter"></param>
        public Url(Type controllerType, string action, string parameter = default)
        {
            this.Action = action;
            this.ControllerType = controllerType;
            this.Parameter = parameter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Url"/> class.
        /// </summary>
        /// <param name="area"></param>
        /// <param name="type"></param>
        /// <param name="action"></param>
        /// <param name="parameter"></param>
        public Url(string area, Type type, string action, string parameter = default) : this(type, action, parameter)
        {
            this.Area = area;
        }

        public string Action { get; set; }

        public Type ControllerType { get; set; }

        public string Area { get; set; }

        public string Parameter { get; set; }

        public string Render()
        {
            string controller = this.ControllerType.Name.Replace(nameof(Controller), default);
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