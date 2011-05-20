using Repository.Infrastructure;

namespace EfImpl
{
    public partial class Person: IGuidKeyed
    {
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Person)) return false;
            return Equals((Person) obj);
        }

        public bool Equals(Person other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._Id.Equals(_Id) && Equals(other._FirstName, _FirstName) && Equals(other._LastName, _LastName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = _Id.GetHashCode();
                result = (result*397) ^ (_FirstName != null ? _FirstName.GetHashCode() : 0);
                result = (result*397) ^ (_LastName != null ? _LastName.GetHashCode() : 0);
                return result;
            }
        }
    }
}