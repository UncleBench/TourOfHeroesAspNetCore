using TourOfHeroes.Domain.Common.Models;

namespace TourOfHeroes.Domain.Heroes.ValueObjects
{
    public sealed class SuperPowerId : ValueObject
    {
        public Guid Value { get; }

        private SuperPowerId(Guid value)
        {
            Value = value;
        }

        public static SuperPowerId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        
        public static SuperPowerId Create(Guid value)
        {
            return new SuperPowerId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
