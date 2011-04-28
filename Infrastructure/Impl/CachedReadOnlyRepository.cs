using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Infrastructure.Impl
{
	public class CachedReadOnlyRepository<T> : IReadOnlyRepository<T> where T:class
	{
		private readonly TimeSpan _refreshInterval;
		private readonly IReadOnlyRepository<T> _readOnlyRepositoryToCache;
		private DateTime _lastRefresh = DateTime.MinValue;
		private IQueryable<T> _cache;

		public CachedReadOnlyRepository(TimeSpan refreshInterval, IReadOnlyRepository<T> readOnlyRepositoryToCache)
		{
			_refreshInterval = refreshInterval;
			_readOnlyRepositoryToCache = readOnlyRepositoryToCache;
		}

		public IQueryable<T> All()
		{
			if (_cache == null || (DateTime.Now - _lastRefresh) > _refreshInterval)
			{
				_cache = _readOnlyRepositoryToCache.All();
				_lastRefresh = DateTime.Now;
			}
			return _cache;
		}

		public T Single(Expression<Func<T, bool>> expression)
		{
			return All().SingleOrDefault(expression);
		}

		#region IReadOnlyRepository<T> Members


		public T FindBy(Expression<Func<T, bool>> expression)
		{
			return FilterBy(expression).Single();
		}

		public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
		{
			return All().Where(expression).AsQueryable();
		}

		#endregion
	}
}