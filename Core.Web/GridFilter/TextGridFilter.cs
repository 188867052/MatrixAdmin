using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.GridFilter
{
    public class TextGridFilter<TPostModel> : BaseGridFilter
    {
        private readonly Expression<Func<TPostModel, string>> expression;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="label"></param>
        public TextGridFilter(Expression<Func<TPostModel, string>> expression, string label) : base(label)
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

        public string Event { get; set; }
    }
}