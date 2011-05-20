namespace Repository.Infrastructure
{
    public interface IKeyedRepository<TKey, TEntity> : IKeyedReadOnlyRepository<TKey, TEntity>, IRepository<TEntity> 
        where TEntity : class, IKeyed<TKey>
	{
	}

    public interface IGuidKeyedRepository<TEntity> : IGuidKeyedReadOnlyRepository<TEntity>, IRepository<TEntity>
        where TEntity : class, IGuidKeyed
    {
    }

    public interface IIntKeyedRepository<TEntity> : IIntKeyedReadOnlyRepository<TEntity>, IRepository<TEntity>
        where TEntity : class, IIntKeyed
    {
    }
}