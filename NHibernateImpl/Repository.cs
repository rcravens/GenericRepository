using NHibernate;
using System.Linq;
using NHibernate.Linq;
using Repository.Infrastructure;

namespace Repository.NHibernateImpl
{
    public class Repository<TKey, TEntity> : NHibernateContext, IKeyedRepository<TKey, TEntity>
            where TEntity : class, IKeyed<TKey>
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public bool Add(TEntity entity)
        {
            _session.Save(entity);
            return true;
        }

        public bool Add(System.Collections.Generic.IEnumerable<TEntity> items)
        {
            foreach (TEntity item in items)
            {
                _session.Save(item);
            }
            return true;
        }

        public bool Update(TEntity entity)
        {
            _session.Update(entity);
            return true;
        }

        public bool Delete(TEntity entity)
        {
            _session.Delete(entity);
            return true;
        }

        public bool Delete(System.Collections.Generic.IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                _session.Delete(entity);
            }
            return true;
        }

        public IQueryable<TEntity> All()
        {
            return _session.Linq<TEntity>();
        }

        public TEntity FindBy(TKey id)
        {
            return _session.Get<TEntity>(id);
        }
    }
}