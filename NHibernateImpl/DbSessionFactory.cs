using Repository.Infrastructure;

namespace Repository.NHibernateImpl
{
    public class DbSessionFactory : IDbSessionFactory
    {
        private readonly NHibernateHelper _nHibernateHelper;

        public DbSessionFactory()
        {
            _nHibernateHelper = new NHibernateHelper();
        }

        public IDbSession Create()
        {
            return new DbSession(_nHibernateHelper.SessionFactory);
        }
    }
}