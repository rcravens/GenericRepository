using System;
using FakeImplTests.Entities;
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
            new FakeDbSessionFactory<int>(null);
        }

        [TestMethod]
        public void Create_Returns_Db_Session()
        {
            InMemoryDb db = new InMemoryDb();
            FakeDbSessionFactory<int> dbSessionFactory = new FakeDbSessionFactory<int>(db);

            IDbSession<int, Person> dbSession = dbSessionFactory.Create<Person>();
            Assert.IsNotNull(dbSession);
        }
    }
}
