﻿namespace TourOfHeroes.Domain.Common.Models
{
    public abstract class ValueObject
    {
        public abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            { 
                return false;
            }

            var valueObject = obj as ValueObject;

            return GetEqualityComponents()
                .SequenceEqual(valueObject!.GetEqualityComponents());
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {  
            return !Equals(left, right); 
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        public bool Equals(ValueObject? other) 
        {
            return Equals((object?)other);
        }
    }
}
