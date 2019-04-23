using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter_backup
{
    public class EnumGridFilter<TModel, TPostModel> : BaseGridFilter<TModel, TPostModel>
    {
        private readonly Expression<Func<TModel, Enum>> expression;

        public EnumGridFilter(Expression<Func<TModel, Enum>> expression, string thead) : base(thead)
        {
            this.expression = expression;
        }
    }
}