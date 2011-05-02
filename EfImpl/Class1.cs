using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.EfImpl
{
    /*
    public class Mapper
    {
        public Ad
    }
    public class DbSessionFactory : IDbSessionFactory
    {
        public IDbSession Create()
        {
            throw new NotImplementedException();
        }
    }

    public class DbSession : IDbSession
    {
        private readonly ObjectContext _context;

        public DbSession(ObjectContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public IKeyedRepository<TKey, TEntity> CreateKeyedRepository<TKey, TEntity>() where TEntity : class, IKeyed<TKey>
        {
            IObjectSet<TEntity> objectSet = _context.CreateObjectSet<TEntity>();
            throw new NotImplementedException();
        }

        public IKeyedReadOnlyRepository<TKey, TEntity> CreateKeyedReadOnlyRepository<TKey, TEntity>() where TEntity : class, IKeyed<TKey>
        {
            throw new NotImplementedException();
        }

        public IReadOnlyRepository<TEntity> CreateReadOnlyRepository<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
        }
    }


    public class Repository<TKey, TEntity> : IKeyedRepository<TKey, TEntity>
            where TEntity : class, IKeyed<TKey>
    {
        private readonly IObjectSet<TEntity> _objectSet;

        public Repository(IObjectSet<TEntity> objectSet)
        {
            _objectSet = objectSet;
        }

        public IQueryable<TEntity> All()
        {
            _objectSet.AsQueryable();
        }

        public TEntity FindBy(TKey id)
        {
            return _objectSet.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public bool Add(TEntity entity)
        {
            _objectSet.AddObject(entity);
            return true;
        }

        public bool Update(TEntity entity)
        {
            TEntity e = FindBy(entity.Id);
            if(e == null)
            {
                return false;
            }
            e = entity;
            return true;
        }

        public bool Delete(TEntity entity)
        {
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
    }*/
}
