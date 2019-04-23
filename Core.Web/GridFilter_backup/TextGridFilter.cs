using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter_backup
{
    public class TextGridFilter<TModel, TPostModel> : BaseGridFilter<TModel, TPostModel>
    {
        private readonly Expression<Func<TModel, string>> expression;

        public TextGridFilter(Expression<Func<TModel, string>> expression, string label) : base(label)
        {
            this.expression = expression;
        }
    }
}