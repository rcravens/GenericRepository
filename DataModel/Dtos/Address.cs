using System;
using Repository.Infrastructure;

namespace Repository.DataModel.Dtos
{
    public class Address : IKeyed<int>
    {
        public virtual Int32 Id { get; private set; }
        public virtual int PersonId { get; set; }
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual string Zip { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Address)) return false;
            return Equals((Address) obj);
        }

        public virtual bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id && other.PersonId == PersonId && Equals(other.Street, Street) && Equals(other.City, City) && Equals(other.State, State) && Equals(other.Country, Country) && Equals(other.Zip, Zip);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Id;
                result = (result*397) ^ PersonId;
                result = (result*397) ^ (Street != null ? Street.GetHashCode() : 0);
                result = (result*397) ^ (City != null ? City.GetHashCode() : 0);
                result = (result*397) ^ (State != null ? State.GetHashCode() : 0);
                result = (result*397) ^ (Country != null ? Country.GetHashCode() : 0);
                result = (result*397) ^ (Zip != null ? Zip.GetHashCode() : 0);
                return result;
            }
        }
    }
}