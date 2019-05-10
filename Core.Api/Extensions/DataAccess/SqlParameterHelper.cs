using System;

namespace Core.Api.Extensions.DataAccess
{
    /// <summary>
    /// Use for an alternative param name other than the propery name.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property)]
    public class QueryParamNameAttribute : Attribute
    {
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryParamNameAttribute"/> class.
        /// </summary>
        /// <param name="name"></param>
        public QueryParamNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
