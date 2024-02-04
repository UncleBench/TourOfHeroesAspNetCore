namespace TourOfHeroes.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; private init; }

        protected Entity(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }
    }
}
