using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter
{
    public class TextGridFilter<TPostModel> : BaseGridFilter
    {
        private readonly Expression<Func<TPostModel, string>> expression;
        public TextGridFilter(Expression<Func<TPostModel, string>> expression, string label) : base(label)
        {
            this.expression = expression;
        }


        public string Event { get; set; }
    }
}