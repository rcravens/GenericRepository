using System.Linq;

namespace Repository.Infrastructure
{
	public interface IReadOnlyRepository<TEntity> where TEntity:class 
	{
		IQueryable<TEntity> All();
	}
}