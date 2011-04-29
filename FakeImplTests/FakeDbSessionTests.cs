using System;
using FakeImplTests.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.FakeImpl;
using Repository.Infrastructure;

namespace FakeImplTests
{
    [TestClass]
    public class FakeDbSessionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_Excpection_If_Db_Is_Null()
        {
            new FakeDbSession(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CreateReadOnlyRepository_Throws_Exception()
        {
            FakeDbSession dbSession = GetSession();

            dbSession.CreateReadOnlyRepository<Person>();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CreateRepository_Throws_Exception()
        {
            FakeDbSession dbSession = GetSession();

            dbSession.CreateRepository<Person>();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Rollback_Throws_Exception()
        {
            FakeDbSession dbSession = GetSession();

            dbSession.Rollback();
        }

        [TestMethod]
        public void CreateKeyedRepository_Returns_Expected_Type()
        {
            FakeDbSession dbSession = GetSession();

            IKeyedRepository<int, Person> repository = dbSession.CreateKeyedRepository<int, Person>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void CreateKeyedReadOnlyRepository_Returns_Expected_Type()
        {
            FakeDbSession dbSession = GetSession();

            IKeyedReadOnlyRepository<int, Person> repository = dbSession.CreateKeyedReadOnlyRepository<int, Person>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void Dispose_Is_Implemented()
        {
            FakeDbSession dbSession = GetSession();
            dbSession.Dispose();
        }

        [TestMethod]
        public void Commit_Is_Implemented()
        {
            FakeDbSession dbSession = GetSession();
            dbSession.Commit();
        }

        private static FakeDbSession GetSession()
        {
            InMemoryDb db = new InMemoryDb();

            FakeDbSession dbSession = new FakeDbSession(db);

            return dbSession;
        }
    }
}