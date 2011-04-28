using System;
using Repository.Infrastructure;

namespace Repository.FakeImpl
{
    public class FakeDbSessionFactory<TKey> : IDbSessionFactory<TKey>
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

        public IDbSession<TKey, TEntity> Create<TEntity>() where TEntity : class, IKeyed<TKey>
        {
            return new FakeDbSession<TKey, TEntity>(_db);
        }
    }
}