using System;
using Core.Extension.FieldInfos;

namespace Core.Extension.Filters
{
    public class DateTimeBetweenFilter<T> : BaseBetweenFilter<T>
    {
        public DateTimeBetweenFilter(DateTimeField fieldInfo, DateTime? value, DateTime? value2) : base(fieldInfo.Value, value, value2)
        {
        }
    }
}