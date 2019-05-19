using System.Linq;

namespace Core.Entity
{
    public abstract class Field
    {
        public Field(params object[] args)
        {
            this.Value = string.Join('.', args);
            this.Value = this.Value.Replace("Field", string.Empty);
            this.PropertyName = (string)args.Last();
        }

        public string Value { get; set; }

        public string PropertyName { get; }
    }
}
