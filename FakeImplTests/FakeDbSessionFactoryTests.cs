using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.FakeImpl;
using Repository.Infrastructure;

namespace FakeImplTests
{
    [TestClass]
    public class FakeDbSessionFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_Exception_If_Db_Is_Null()
        {
            new DbSessionFactory(null);
        }

        [TestMethod]
        public void Create_Returns_Db_Session()
        {
            InMemoryDb db = new InMemoryDb();
            DbSessionFactory dbSessionFactory = new DbSessionFactory(db);

            IDbSession dbSession = dbSessionFactory.Create();
            Assert.IsNotNull(dbSession);
        }
    }
}
