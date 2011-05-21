using Repository.Infrastructure;

namespace EfImpl
{
    public class DbSessionFactory : IDbSessionFactory
    {
        private readonly string _connectionString;

        public DbSessionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbSession Create()
        {
            db2Entities context = new db2Entities(_connectionString);
            return new DbSession(context);
        }
    }
}