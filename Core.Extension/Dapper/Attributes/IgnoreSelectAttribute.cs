using System;

namespace Core.Extension.Dapper.Attributes
{
    /// <summary>
    /// Optional IgnoreSelect attribute.
    /// Custom for Dapper.SimpleCRUD to exclude a property from Select methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreSelectAttribute : Attribute
    {
    }
}
