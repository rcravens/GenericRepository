using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Infrastructure;
using Repository.NHibernateImpl;

namespace NHibernateImplTests
{
    [TestClass]
    public class DbSessionFactoryTests
    {
        [TestMethod]
        public void Create_Returns_Db_Session()
        {
            DbSessionFactory dbSessionFactory = new DbSessionFactory();

            IDbSession dbSession = dbSessionFactory.Create();
            Assert.IsNotNull(dbSession);
        }
    }
}
