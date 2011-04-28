using System;
using System.Data;
using NHibernate;
using Repository.Infrastructure;

namespace Repository.NHibernateImpl
{
    public class DbSession<TEntity> : IDbSession<int, TEntity> where TEntity : class, IKeyed<int>
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

        public IKeyedRepository<int, TEntity> CreateKeyedRepository()
        {
            return new Repository<int, TEntity>(_session);
        }

        public IKeyedReadOnlyRepository<int, TEntity> CreateKeyedReadOnlyRepository()
        {
            return new Repository<int, TEntity>(_session);
        }

        public IReadOnlyRepository<TEntity> CreateReadOnlyRepository()
        {
            throw new NotImplementedException();
        }

        public IRepository<TEntity> CreateRepository()
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