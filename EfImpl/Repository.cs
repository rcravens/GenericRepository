using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Infrastructure;
using System.Data.Objects;

namespace EfImpl
{
    public class Repository<TKey, TEntity> : IKeyedRepository<TKey, TEntity>
            where TEntity : class, IKeyed<TKey>
    {
        private readonly IObjectSet<TEntity> _objectSet;

        public Repository(IObjectSet<TEntity> objectSet)
        {
            if(objectSet == null)
            {
                throw new ArgumentNullException("objectSet");
            }
            _objectSet = objectSet;
        }
        public IQueryable<TEntity> All()
        {
            return _objectSet;
        }

        public TEntity FindBy(TKey id)
        {
            return _objectSet.FirstOrDefault(x => x.Id.Equals(id));
        }

        public bool Add(TEntity entity)
        {
            _objectSet.AddObject(entity);
            return true;
        }

        public bool Update(TEntity entity)
        {
            _objectSet.Attach(entity);
            return true;
        }

        public bool Delete(TEntity entity)
        {
            _objectSet.Attach(entity);
            _objectSet.DeleteObject(entity);
            return true;
        }

        public bool Delete(IEnumerable<TEntity> entities)
        {
            bool result = true;
            foreach (TEntity entity in entities)
            {
                result = Delete(entity) && result;
            }
            return result;
        }

        public bool Add(IEnumerable<TEntity> items)
        {
            bool result = true;
            foreach (TEntity entity in items)
            {
                result = Add(entity) && result;
            }
            return result;
        }
    }
}
