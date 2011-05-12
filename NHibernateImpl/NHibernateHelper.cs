using System;
using NHibernate;

namespace Repository.NHibernateImpl
{
	public class NHibernateHelper
	{
        private readonly string _connectionString;
        private ISessionFactory _sessionFactory;

        public NHibernateHelper(string  connectionString)
        {
            if(connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }
            _connectionString = connectionString;
        }

		public ISessionFactory SessionFactory
		{
			get { return _sessionFactory ?? (_sessionFactory = CreateSessionFactory()); }
		}

		private ISessionFactory CreateSessionFactory()
		{
            return new NHibernate.Cfg.Configuration()
                .Configure()
                .SetProperty("connection.connection_string", _connectionString)
                .BuildSessionFactory();
		}
	}
}