using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Infrastructure;
using Repository.NHibernateImpl;

namespace NHibernateImplTests
{
    [TestClass]
    public class DbSessionFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_If_Connection_String_Param_Is_Null()
        {
            new DbSessionFactory(null);
        }

        [TestMethod]
        public void Create_Returns_Db_Session()
        {
            DbSessionFactory dbSessionFactory = new DbSessionFactory(Helpers.ConnectionString);

            IDbSession dbSession = dbSessionFactory.Create();
            Assert.IsNotNull(dbSession);
        }
    }
}
