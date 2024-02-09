using TourOfHeroes.Domain.Common.Models;

namespace TourOfHeroes.Domain.Heroes.ValueObjects
{
    public sealed class HeroId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private HeroId(Guid value)
        {
            Value = value;
        }

        public static HeroId CreateUnique()
        {
            // TODO: enforce invariants
            return new(Guid.NewGuid());
        }

        public static HeroId Create(Guid value)
        {
            // TODO: enforce invariants
            return new HeroId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
