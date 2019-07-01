using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Shared.Utilities;

namespace Core.Web.GridColumn
{
    public class BooleanGridColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, bool>> expression;
        private readonly IList<KeyValuePair<bool, string>> optionsValuePair;

        public BooleanGridColumn(Expression<Func<T, bool>> expression, string thead) : base(thead)
        {
            Check.NotNull(expression, nameof(expression));

            this.expression = expression;
            this.optionsValuePair = new List<KeyValuePair<bool, string>>();
        }

        public void AddOption(bool key, string value)
        {
            Check.NotEmpty(value, nameof(value));

            this.optionsValuePair.Add(new KeyValuePair<bool, string>(key, value));
        }

        public override string RenderTd(T entity)
        {
            Check.NotNull(entity, nameof(entity));

            var key = this.expression.Compile()(entity);
            var display = this.optionsValuePair.FirstOrDefault(o => o.Key == key);
            return this.RenderTd(display.Value);
        }
    }
}