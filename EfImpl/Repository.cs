using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using Repository.Infrastructure;
using System.Data.Objects;

namespace EfImpl
{
    public class Repository<TEntity> : IGuidKeyedRepository<TEntity>
            where TEntity : class, IGuidKeyed
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

        public TEntity FindBy(Guid id)
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
            EntityObject obj = entity as EntityObject;
            if(obj == null || obj.EntityState == System.Data.EntityState.Detached)
            {
                if(FindBy(entity.Id)==null)
                {
                    return false;
                }
                _objectSet.Attach(entity);
            }
            return true;
        }

        public bool Delete(TEntity entity)
        {
            TEntity toDelete = FindBy(entity.Id);
            if(toDelete == null)
            {
                return false;
            }
            _objectSet.DeleteObject(toDelete);
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
