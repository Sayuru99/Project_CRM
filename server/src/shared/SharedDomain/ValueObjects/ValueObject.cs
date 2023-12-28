namespace MyApp.SharedDomain.ValueObjects
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public bool Equals(ValueObject? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ValueObject)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                foreach (var component in GetEqualityComponents())
                {
                    hashCode = hashCode * 23 + (component?.GetHashCode() ?? 0);
                }
                return hashCode;
            }
        }

        public abstract object GetValue();
        protected abstract IEnumerable<object> GetEqualityComponents();

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }
    }
}
