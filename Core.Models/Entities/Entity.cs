namespace Core.Models.Entities
{
    public abstract class Entity<T>
    {
        public T Id { get; set; }
    }
}
