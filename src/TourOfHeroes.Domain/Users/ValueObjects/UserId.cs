using TourOfHeroes.Domain.Common.Models;

namespace TourOfHeroes.Domain.Users.ValueObjects
{
    public sealed class UserId : ValueObject
    {
        public Guid Value { get; }

        private UserId(Guid value)
        {
            Value = value;
        }

        public static UserId CreateUnique()
        {
            // TODO: enforce invariants
            return new(Guid.NewGuid());
        }

        public static UserId Create(Guid value)
        {
            // TODO: enforce invariants
            return new UserId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
