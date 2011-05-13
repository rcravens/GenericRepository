using System;
using Repository.Infrastructure;

namespace Repository.NHibernateImpl
{
    public class DbSessionFactory : IDbSessionFactory
    {
        private readonly NHibernateHelper _nHibernateHelper;

        public DbSessionFactory(string connectionString)
        {
            if(connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }
            _nHibernateHelper = new NHibernateHelper(connectionString);
        }

        public IDbSession Create()
        {
            return new DbSession(_nHibernateHelper.SessionFactory);
        }
    }
}