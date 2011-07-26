using System;
using System.Reflection;
using NHibernate;

namespace Repository.NHibernateImpl
{
	public class NHibernateHelper
	{
        private readonly string _connectionString;
        private ISessionFactory _sessionFactory;
	    private readonly Assembly _resourceAssembly;

	    public NHibernateHelper(string connectionString, Assembly resourceAssembly)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }
            _connectionString = connectionString;

            if (resourceAssembly == null)
            {
                throw new ArgumentNullException("resourceAssembly");
            }
	        _resourceAssembly = resourceAssembly;

        }

		public ISessionFactory SessionFactory
		{
			get { return _sessionFactory ?? (_sessionFactory = CreateSessionFactory()); }
		}

		private ISessionFactory CreateSessionFactory()
		{
            return new NHibernate.Cfg.Configuration()
                .Configure()
                .AddAssembly(_resourceAssembly)
                .SetProperty("connection.connection_string", _connectionString)
                .BuildSessionFactory();
		}
	}
}