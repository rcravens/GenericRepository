using System;
using Repository.Infrastructure;

namespace EfImpl
{
    public partial class Address: IKeyed<Guid>
    {
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Address)) return false;
            return Equals((Address) obj);
        }

        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._Id.Equals(_Id) && Equals(other._Street, _Street) && Equals(other._City, _City) && Equals(other._State, _State) && Equals(other._Country, _Country) && Equals(other._Zip, _Zip) && other._PersonId.Equals(_PersonId);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = _Id.GetHashCode();
                result = (result*397) ^ (_Street != null ? _Street.GetHashCode() : 0);
                result = (result*397) ^ (_City != null ? _City.GetHashCode() : 0);
                result = (result*397) ^ (_State != null ? _State.GetHashCode() : 0);
                result = (result*397) ^ (_Country != null ? _Country.GetHashCode() : 0);
                result = (result*397) ^ (_Zip != null ? _Zip.GetHashCode() : 0);
                result = (result*397) ^ (_PersonId.HasValue ? _PersonId.Value.GetHashCode() : 0);
                return result;
            }
        }
    }
}