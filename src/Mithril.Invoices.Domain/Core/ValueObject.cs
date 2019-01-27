namespace Mithril.Invoices.Domain.Core
{
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is T other)
            {
                return EqualsCore(other);
            }

            return false;
        }

        public abstract bool EqualsCore(T obj);

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> first, ValueObject<T> second)
        {
            return !ReferenceEquals(first, null) && first.Equals(second);
        }

        public static bool operator !=(ValueObject<T> first, ValueObject<T> second)
        {
            return !(first == second);
        }
    }
}
