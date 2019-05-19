using System;
using System.Linq.Expressions;
using Core.Entity;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DateTimeBetweenFilter<T> : BaseBetweenFilter<T>
    {
        public DateTimeBetweenFilter(DateTimeField fieldInfo, DateTime? value, DateTime? value2) : base(fieldInfo.Value, value, value2)
        {
        }
    }
}