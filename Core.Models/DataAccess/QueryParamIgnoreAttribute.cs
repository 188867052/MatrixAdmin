using System;

namespace Core.Model.DataAccess
{
    /// <summary>
    /// Ignore this property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class QueryParamIgnoreAttribute : Attribute
    {
    }
}
