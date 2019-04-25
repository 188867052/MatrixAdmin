using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.GridFilter
{
    public class IntegerGridFilter<TPostModel> : BaseGridFilter
    {

        private string Class = "custom-control-inline";

        private readonly Expression<Func<TPostModel, int>> expression;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="label"></param>
        public IntegerGridFilter(Expression<Func<TPostModel, int>> expression, string label) : base(label)
        {
            this.expression = expression;
        }

        public override string Render()
        {
            string name = this.expression.GetPropertyInfo().Name;
            return $"<div class=\"{this.Class}\">" +
                   $"<label>{this.LabelText}</label>" +
                   $"<input name= \"{name}\" type=\"text\">" +
                   $"</div>";
        }
    }
}