using System;

namespace Repository.Infrastructure
{
    public interface IDbSession<TKey, TEntity> : IDisposable where TEntity : class, IKeyed<TKey>
   {
       IKeyedRepository<TKey, TEntity> CreateKeyedRepository();
       IKeyedReadOnlyRepository<TKey, TEntity> CreateKeyedReadOnlyRepository();
       IReadOnlyRepository<TEntity> CreateReadOnlyRepository();
       IRepository<TEntity> CreateRepository();
       void Commit();
       void Rollback();
   }
}