using Core.Extension.FieldInfos;

namespace Core.Extension.Filters
{
    public class StringContainsFilter<T> : BaseFilter<T>
    {
        public StringContainsFilter(StringField stringField, string value) : base(stringField, ExpressionBuilder.Operations.Operation.Contains, value)
        {
        }
    }
}
