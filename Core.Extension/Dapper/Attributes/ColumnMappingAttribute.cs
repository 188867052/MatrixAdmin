using System;

namespace Core.Extension.Dapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ColumnMappingAttribute : Attribute
    {
        public string Name { get; set; }
    }
}