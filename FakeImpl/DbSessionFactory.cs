using System;
using Repository.Infrastructure;

namespace Repository.FakeImpl
{
    public class DbSessionFactory : IDbSessionFactory
    {
        private readonly InMemoryDb _db;

        public DbSessionFactory(InMemoryDb db)
        {
            if(db==null)
            {
                throw new ArgumentNullException("db");
            }
            _db = db;
        }

        public IDbSession Create()
        {
            return new DbSession(_db);
        }
    }
}