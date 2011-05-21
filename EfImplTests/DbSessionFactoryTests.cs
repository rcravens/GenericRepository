using EfImpl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Infrastructure;

namespace EfImplTests
{
    [TestClass]
    public class DbSessionFactoryTests
    {
        [TestMethod]
        public void Create_Returns_Db_Session()
        {
            DbSessionFactory dbSessionFactory = new DbSessionFactory(Helpers.ConnectionString);

            IDbSession dbSession = dbSessionFactory.Create();
            Assert.IsNotNull(dbSession);
        }
    }
}
