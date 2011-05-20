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
    public interface IDbSessionGuidKeyed : IDisposable
    {
        IGuidKeyedRepository<TEntity> CreateKeyedRepository<TEntity>() where TEntity : class, IGuidKeyed;
        IGuidKeyedReadOnlyRepository<TEntity> CreateKeyedReadOnlyRepository<TEntity>() where TEntity : class, IGuidKeyed;
        void Commit();
        void Rollback();
    }
    public interface IDbSessionIntKeyed : IDisposable
    {
        IIntKeyedRepository<TEntity> CreateKeyedRepository<TEntity>() where TEntity : class, IIntKeyed;
        IIntKeyedReadOnlyRepository<TEntity> CreateKeyedReadOnlyRepository<TEntity>() where TEntity : class, IIntKeyed;
        void Commit();
        void Rollback();
    }
}