using System;
using Repository.Infrastructure;

namespace FakeImplTests.Entities
{
    public class SecretInfo : IKeyed<Guid>
    {
        public Guid Id { get; set; }
        public string Secret { get; set; }
    }
}