using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.GridFilter
{
    public class IntegerGridFilter<TPostModel> : BaseGridFilter
    {
        private readonly string _class = "custom-control-inline";
        private readonly Expression<Func<TPostModel, int>> _expression;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="label"></param>
        public IntegerGridFilter(Expression<Func<TPostModel, int>> expression, string label) : base(label)
        {
            this._expression = expression;
        }

        public override string Render()
        {
            string name = this._expression.GetPropertyInfo().Name;
            return $"<div class=\"{_class}\">" +
                   $"<label>{this.LabelText}</label>" +
                   $"<input name= \"{name}\" type=\"text\">" +
                   $"</div>";
        }
    }
}