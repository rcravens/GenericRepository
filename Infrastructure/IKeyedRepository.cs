namespace Repository.Infrastructure
{
    public interface IKeyedRepository<TKey, TEntity> : IKeyedReadOnlyRepository<TKey, TEntity>, IRepository<TEntity> 
        where TEntity : class, IKeyed<TKey>
	{
	}
}