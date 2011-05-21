using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Linq;
using Repository.Infrastructure;
using System.Data.Objects;

namespace EfImpl
{
    public class Repository<TKey, TEntity> : IKeyedRepository<TKey, TEntity>
            where TEntity : class, IKeyed<TKey>
    {

        private readonly ObjectContext _context;
        private readonly ObjectSet<TEntity> _objectSet;
        private readonly string _entitySetName;
        private const string _keyName = "Id"; // IKeyed<>

        public Repository(ObjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            _context = context;

            _objectSet = _context.CreateObjectSet<TEntity>();

            _entitySetName = _context.DefaultContainerName + "." + _objectSet.EntitySet.Name;
        }
        public IQueryable<TEntity> All()
        {
            return _objectSet;
        }

        public TEntity FindBy(TKey id)
        {
            EntityKey key = new EntityKey(_entitySetName, _keyName, id);
            try
            {
                return (TEntity)_context.GetObjectByKey(key);
            }
            catch (ObjectNotFoundException)
            {
                // Object not found...return null
                return null;
            }
        }

        public bool Add(TEntity entity)
        {
            _objectSet.AddObject(entity);
            return true;
        }

        public bool Update(TEntity entity)
        {
            EntityObject obj = entity as EntityObject;
            if(obj == null || obj.EntityState == EntityState.Detached)
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
