using Repository.Infrastructure;

namespace Repository.DataModel.Dtos
{
    public class Person : IKeyed<int>
    {
        public virtual int Id { get; private set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Person)) return false;
            return Equals((Person) obj);
        }

        public virtual bool Equals(Person other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id && Equals(other.FirstName, FirstName) && Equals(other.LastName, LastName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Id;
                result = (result*397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                result = (result*397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                return result;
            }
        }
    }
}
