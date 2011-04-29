using System;
using System.Data;
using NHibernate;
using Repository.Infrastructure;

namespace Repository.NHibernateImpl
{
    public class DbSession : IDbSession
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;
        
        public DbSession(ISessionFactory sessionFactory)
		{
            if(sessionFactory==null)
            {
                throw new ArgumentNullException("sessionFactory");
            }

            _session = sessionFactory.OpenSession();
            _session.FlushMode = FlushMode.Auto;
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
		}
        ~DbSession()
        {
            Dispose();
        }

        public void Dispose()
        {
            lock (_session)
            {
                if (_session.IsOpen)
                {
                    _session.Close();
                }
            }
            GC.SuppressFinalize(this);
        }

        public IKeyedRepository<TKey, TEntity> CreateKeyedRepository<TKey, TEntity>() where TEntity : class, IKeyed<TKey>
        {
            return new Repository<TKey, TEntity>(_session);
        }

        public IKeyedReadOnlyRepository<TKey, TEntity> CreateKeyedReadOnlyRepository<TKey, TEntity>() where TEntity : class, IKeyed<TKey>
        {
            return new Repository<TKey, TEntity>(_session);
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
            if (!_transaction.IsActive)
            {
                throw new InvalidOperationException("No active transation");
            }
            _transaction.Commit();
        }

        public void Rollback()
        {
            if (_transaction.IsActive)
            {
                _transaction.Rollback();
            }
        }
    }
}