using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Extension
{
    public class Url
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Url"/> class.
        /// </summary>
        /// <param name="controllerType">controllerType.</param>
        /// <param name="action">action.</param>
        /// <param name="parameter">parameter.</param>
        public Url(Type controllerType, string action, string parameter = default)
        {
            this.Action = action;
            this.ControllerType = controllerType;
            this.Parameter = parameter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Url"/> class.
        /// </summary>
        /// <param name="area">area.</param>
        /// <param name="controllerType">controllerType.</param>
        /// <param name="action">action.</param>
        /// <param name="parameter">parameter.</param>
        public Url(string area, Type controllerType, string action, string parameter = default) : this(controllerType, action, parameter)
        {
            this.Area = area;
        }

        public string Action { get; set; }

        public Type ControllerType { get; set; }

        public string Area { get; set; }

        public string Parameter { get; set; }

        public IList<string> ActionParameterName
        {
            get
            {
                ParameterInfo[] parameter = this.ControllerType.GetMethod(this.Action).GetParameters();
                if (parameter == null)
                {
                    throw new Exception($"Action:[{this.Action}]没有参数.");
                }

                return parameter.Select(o => o.Name).ToList();
            }
        }

        public string Query(object parameters)
        {
            PropertyInfo[] propertyInfos = parameters.GetType().GetProperties();
            if (propertyInfos.Length != this.ActionParameterName.Count)
            {
                throw new ArgumentException("参数数目不对.");
            }

            string query = "?";
            for (int i = 0; i < this.ActionParameterName.Count; i++)
            {
                string value = propertyInfos[i].GetValue(parameters).ToString();
                query += $"{this.ActionParameterName[i]}={value}";
                if (i != this.ActionParameterName.Count - 1)
                {
                    query += "&";
                }
            }

            return query;
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