using System;

namespace Repository.Infrastructure
{
	public interface IKeyedReadOnlyRepository<TKey, TEntity> : IReadOnlyRepository<TEntity> 
        where TEntity : class, IKeyed<TKey>
	{
		TEntity FindBy(TKey id);
	}

    public interface IGuidKeyedReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class, IGuidKeyed
    {
        TEntity FindBy(Guid id);
    }

    public interface IIntKeyedReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class, IIntKeyed
    {
        TEntity FindBy(int id);
    }
}