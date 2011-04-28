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
            new FakeDbSession<int, Person>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CreateReadOnlyRepository_Throws_Exception()
        {
            FakeDbSession<int, Person> dbSession = GetSession();

            dbSession.CreateReadOnlyRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CreateRepository_Throws_Exception()
        {
            FakeDbSession<int, Person> dbSession = GetSession();

            dbSession.CreateRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Rollback_Throws_Exception()
        {
            FakeDbSession<int, Person> dbSession = GetSession();

            dbSession.Rollback();
        }

        [TestMethod]
        public void CreateKeyedRepository_Returns_Expected_Type()
        {
            FakeDbSession<int, Person> dbSession = GetSession();

            IKeyedRepository<int, Person> repository = dbSession.CreateKeyedRepository();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void CreateKeyedReadOnlyRepository_Returns_Expected_Type()
        {
            FakeDbSession<int, Person> dbSession = GetSession();

            IKeyedReadOnlyRepository<int, Person> repository = dbSession.CreateKeyedReadOnlyRepository();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void Dispose_Is_Implemented()
        {
            FakeDbSession<int, Person> dbSession = GetSession();
            dbSession.Dispose();
        }

        [TestMethod]
        public void Commit_Is_Implemented()
        {
            FakeDbSession<int, Person> dbSession = GetSession();
            dbSession.Commit();
        }

        private static FakeDbSession<int, Person> GetSession()
        {
            InMemoryDb db = new InMemoryDb();

            FakeDbSession<int, Person> dbSession = new FakeDbSession<int, Person>(db);

            return dbSession;
        }
    }
}