using NHibernate;

namespace Repository.NHibernateImpl
{
	public class NHibernateHelper
	{
		private ISessionFactory _sessionFactory;

		public ISessionFactory SessionFactory
		{
			get { return _sessionFactory ?? (_sessionFactory = CreateSessionFactory()); }
		}

		private static ISessionFactory CreateSessionFactory()
		{
		    return new NHibernate.Cfg.Configuration()
                .Configure()
                .AddAssembly(typeof(NHibernateHelper).Assembly)
                .BuildSessionFactory();
		}
	}
}