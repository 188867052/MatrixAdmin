using System;
using System.Linq;

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
        /// <param name="controllerType"></param>
        /// <param name="action"></param>
        /// <param name="parameter"></param>
        public Url(string area, Type controllerType, string action, string parameter = default) : this(controllerType, action, parameter)
        {
            this.Area = area;
        }

        public string Action { get; set; }

        public Type ControllerType { get; set; }

        public string Area { get; set; }

        public string Parameter { get; set; }

        public string ActionParameterName
        {
            get
            {
                var parameter = this.ControllerType.GetMethod(this.Action).GetParameters().FirstOrDefault();
                if (parameter == null)
                {
                    throw new Exception($"Action:[{this.Action}]没有参数.");
                }

                return parameter.Name;
            }
        }

        public string Render()
        {
            string controller = this.ControllerType.Name.Replace("Controller", default);
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