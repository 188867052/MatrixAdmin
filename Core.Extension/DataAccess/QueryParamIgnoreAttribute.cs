using System;

namespace Core.Extension.DataAccess
{
    /// <summary>
    /// Ignore this property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class QueryParamIgnoreAttribute : Attribute
    {
    }
}
