using System;
using Repository.Infrastructure;

namespace Repository.FakeImpl
{
    public class FakeDbSession : IDbSession
    {
        private readonly InMemoryDb _db;

        public FakeDbSession(InMemoryDb db)
        {
            if(db == null)
            {
                throw new ArgumentNullException("db");
            }
            _db = db;
        }

        public void Dispose()
        {
            // nothing to do
        }

        public IKeyedRepository<TKey, TEntity> CreateKeyedRepository<TKey, TEntity>() where TEntity : class, IKeyed<TKey>
        {
            InMemoryDbTable<TKey, TEntity> table = _db.GetTable<TKey, TEntity>();
            return new FakeRepository<TKey, TEntity>(table);
        }

        public IKeyedReadOnlyRepository<TKey, TEntity> CreateKeyedReadOnlyRepository<TKey, TEntity>() where TEntity : class, IKeyed<TKey>
        {
            InMemoryDbTable<TKey, TEntity> table = _db.GetTable<TKey, TEntity>();
            return new FakeRepository<TKey, TEntity>(table);
        }

        public IReadOnlyRepository<TEntity> CreateReadOnlyRepository<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            // nothing to do
        }

        public void Rollback()
        {
            // this is not implemented
            throw new NotImplementedException();
        }
    }
}