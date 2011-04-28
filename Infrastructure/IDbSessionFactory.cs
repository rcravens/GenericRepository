namespace Repository.Infrastructure
{
    public interface IDbSessionFactory<TKey>
    {
        IDbSession<TKey, TEntity> Create<TEntity>() where TEntity : class, IKeyed<TKey>;
    }
}