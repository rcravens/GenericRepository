using System;

namespace Repository.Infrastructure
{
    public interface IDbSession : IDisposable
    {
        IKeyedRepository<TKey, TEntity> CreateKeyedRepository<TKey, TEntity>() where TEntity : class, IKeyed<TKey>;
        IKeyedReadOnlyRepository<TKey, TEntity> CreateKeyedReadOnlyRepository<TKey, TEntity>() where TEntity : class, IKeyed<TKey>;
        IReadOnlyRepository<TEntity> CreateReadOnlyRepository<TEntity>() where TEntity : class;
        IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;
        void Commit();
        void Rollback();
    }
}