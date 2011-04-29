using System;
using Repository.Infrastructure;

namespace Repository.FakeImpl
{
    public class FakeDbSessionFactory : IDbSessionFactory
    {
        private readonly InMemoryDb _db;

        public FakeDbSessionFactory(InMemoryDb db)
        {
            if(db==null)
            {
                throw new ArgumentNullException("db");
            }
            _db = db;
        }

        public IDbSession Create()
        {
            return new FakeDbSession(_db);
        }
    }
}