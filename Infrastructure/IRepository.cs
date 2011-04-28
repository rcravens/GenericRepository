using System.Collections.Generic;

namespace Repository.Infrastructure
{
	public interface IRepository<TEntity> where TEntity:class
	{
		bool Add(TEntity entity);
		bool Add(IEnumerable<TEntity> items);
		bool Update(TEntity entity);
		bool Delete(TEntity entity);
		bool Delete(IEnumerable<TEntity> entities);
	}
}
