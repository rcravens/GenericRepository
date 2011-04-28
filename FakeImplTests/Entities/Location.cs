using Repository.Infrastructure;

namespace FakeImplTests.Entities
{
    public class Location : IKeyed<int>
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}