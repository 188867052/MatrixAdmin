using System;

namespace Core.Api.Extensions.DataAccess
{
    /// <summary>
    /// Use for an alternative param name other than the property name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class QueryParamNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryParamNameAttribute"/> class.
        /// </summary>
        /// <param name="name">name.</param>
        public QueryParamNameAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
