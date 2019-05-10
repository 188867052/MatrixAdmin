using System;

namespace Core.Extension.DataAccess
{
    /// <summary>
    /// Ignore this property.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property)]
    public class QueryParamIgnoreAttribute : Attribute
    {
    }
}
