using Core.Extension.Expression;
using System;
using System.Linq.Expressions;

namespace Core.Web.Grid
{
    public class EnumColumn<T> : DynamicGridColumn<T>
    {
        public EnumColumn(Expression<Func<T, Enum>> expression, string thead) : base(expression.GetPropertyInfo(), thead)
        {
        }

        public override string RenderTd(T entity)
        {
            var value = this.PropertyInfo.GetValue(entity);
            return $"<td><li><i class=\"{value}\"></i></li></td>";
        }
    }
}