using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter_backup
{
    public class DateTimeGridFilter<TModel, TPostModel> : BaseGridFilter<TModel, TPostModel>
    {
        private readonly Expression<Func<TModel, DateTime>> expression;

        public DateTimeGridFilter(Expression<Func<TModel, DateTime>> expression, string thead) : base(thead)
        {
            this.expression = expression;
        }
    }
}