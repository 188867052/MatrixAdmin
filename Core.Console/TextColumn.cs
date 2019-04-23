using System;
using System.Linq.Expressions;

namespace Core.Console
{
    public class TextColumn<T>
    {
        private readonly Expression<Func<T, string>> _expression;
        public TextColumn(Expression<Func<T, string>> expression)
        {
            _expression = expression;
        }

        public string GetValue(T store)
        {
            var a = _expression.Compile()(store);

            return a;
        }
    }
}