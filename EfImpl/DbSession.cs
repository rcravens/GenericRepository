using System;
using System.Data.Objects;
using Repository.Infrastructure;

namespace EfImpl
{
    public class DbSession : IDbSession
    {
        private readonly ObjectContext _context;

        public DbSession(ObjectContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException("context");
            }
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

        public IKeyedRepository<TKey, TEntity> CreateKeyedRepository<TKey, TEntity>()
            where TEntity : class, IKeyed<TKey>
        {
            return new Repository<TKey, TEntity>(_context);
        }

        public IKeyedReadOnlyRepository<TKey, TEntity> CreateKeyedReadOnlyRepository<TKey, TEntity>() 
            where TEntity : class, IKeyed<TKey>
        {
            return new Repository<TKey, TEntity>(_context);
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
            // By default this is rolled back.
            //
        }
    }
}