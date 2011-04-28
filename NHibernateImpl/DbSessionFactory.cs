using Repository.Infrastructure;

namespace Repository.NHibernateImpl
{
    public class DbSessionFactory : IDbSessionFactory<int>
    {
        private readonly NHibernateHelper _nHibernateHelper;

        public DbSessionFactory()
        {
            _nHibernateHelper = new NHibernateHelper();
        }

        public IDbSession<int, TEntity> Create<TEntity>() where TEntity : class, IKeyed<int>
        {
            return new DbSession<TEntity>(_nHibernateHelper.SessionFactory);
        }
    }
}