using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.GridFilter
{
    public class TextGridFilter<TPostModel> : BaseGridFilter
    {
        private readonly Expression<Func<TPostModel, string>> expression;

        /// <summary>
        /// 构造函数[a
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="label"></param>
        public TextGridFilter(Expression<Func<TPostModel, string>> expression, string label) : base(label)
        {
            this.expression = expression;
        }
        public override string Render()
        {
            string name = this.expression.GetPropertyInfo().Name;
            return $"<div class=\"custom-control-inline\">" +
                   $"<label>{this.LabelText}</label>" +
                   $"<input name= \"{name}\" type=\"text\">" +
                   $"</div>";
        }

        public string Event { get; set; }
    }
}