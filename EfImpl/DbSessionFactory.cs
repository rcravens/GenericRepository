using Repository.Infrastructure;

namespace EfImpl
{
    public class DbSessionFactory : IDbSessionFactory
    {
        public IDbSession Create()
        {
            db2Entities context = new db2Entities();
            return new DbSession(context);
        }
    }
}