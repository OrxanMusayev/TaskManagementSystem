namespace TaskManagementSystem.Domain.Common.Entities
{
    public abstract class Entity : Entity<int>, IEntity
    {
    }

    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public virtual TKey Id { get; set; }

        public Entity()
        {
        }
        public Entity(TKey id)
        {
            Id = id;
        }
        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }
    }
}
