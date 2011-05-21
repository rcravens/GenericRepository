using System;

namespace Repository.Infrastructure
{
	public interface IKeyedReadOnlyRepository<TKey, TEntity> : IReadOnlyRepository<TEntity> 
        where TEntity : class, IKeyed<TKey>
	{
		TEntity FindBy(TKey id);
	}
}