using System;

namespace Core.Api.Extensions.DataAccess
{
    /// <summary>
    /// Ignore this property.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property)]
    public class QueryParamIgnoreAttribute : Attribute
    {
    }
}
