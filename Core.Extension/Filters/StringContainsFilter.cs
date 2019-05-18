using Core.Entity;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class StringContainsFilter<T> : BaseFilter<T>
    {
        public StringContainsFilter(StringField stringField, string value) : base(stringField, Operations.Operation.Contains, value)
        {
        }
    }
}
