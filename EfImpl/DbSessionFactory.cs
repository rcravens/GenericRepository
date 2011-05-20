using Repository.Infrastructure;

namespace EfImpl
{
    public class DbSessionFactory : IDbSessionGuidKeyedFactory
    {
        private readonly string _connectionString;

        public DbSessionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbSessionGuidKeyed Create()
        {
            db2Entities context = new db2Entities(_connectionString);
            return new DbSession(context);
        }
    }
}