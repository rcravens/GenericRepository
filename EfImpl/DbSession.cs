using System;
using System.Data.Objects;
using Repository.Infrastructure;

namespace EfImpl
{
    public class DbSession : IDbSessionGuidKeyed
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

        public IGuidKeyedRepository<TEntity> CreateKeyedRepository<TEntity>() where TEntity : class, IGuidKeyed
        {
            IObjectSet<TEntity> objectSet = _context.CreateObjectSet<TEntity>();
            return new Repository<TEntity>(objectSet);
        }

        public IGuidKeyedReadOnlyRepository<TEntity> CreateKeyedReadOnlyRepository<TEntity>() where TEntity : class, IGuidKeyed
        {
            IObjectSet<TEntity> objectSet = _context.CreateObjectSet<TEntity>();
            return new Repository<TEntity>(objectSet);
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