using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.GridFilter
{
    public class IntegerGridFilter<TPostModel> : BaseGridFilter
    {
        private readonly Expression<Func<TPostModel, int>> expression;
        public IntegerGridFilter(Expression<Func<TPostModel, int>> expression, string label) : base(label)
        {
            this.expression = expression;
        }

        public override string Render()
        {
            string name = typeof(TPostModel).Name + "." + this.expression.GetPropertyInfo().Name;
            return $"<div name= \"{name}\" class=\"custom-control-inline\">" +
                   $"<label>{this.Value}</label>" +
                   $"<input type=\"text\">" +
                   $"</div>";
        }
    }
}