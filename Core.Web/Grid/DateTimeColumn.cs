using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.Grid
{
    public class DateTimeColumn<T> : BaseGridColumn<T>
    {
        public DateTimeColumn(Expression<Func<T, DateTime>> expression, string thead) : base(expression.GetPropertyInfo(), thead)
        {
        }
    }
}