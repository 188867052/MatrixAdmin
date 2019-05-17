namespace Core.Entity
{
    public class CollectionField : Field
    {
        public CollectionField(string field) : base(field)
        {
        }

        public CollectionField(string entity, string field) : base(entity, field)
        {
        }
    }
}
