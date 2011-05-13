using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.DataModel.Dtos;
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
            new DbSession(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CreateReadOnlyRepository_Throws_Exception()
        {
            DbSession dbSession = GetSession();

            dbSession.CreateReadOnlyRepository<Person>();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CreateRepository_Throws_Exception()
        {
            DbSession dbSession = GetSession();

            dbSession.CreateRepository<Person>();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Rollback_Throws_Exception()
        {
            DbSession dbSession = GetSession();

            dbSession.Rollback();
        }

        [TestMethod]
        public void CreateKeyedRepository_Returns_Expected_Type()
        {
            DbSession dbSession = GetSession();

            IKeyedRepository<int, Person> repository = dbSession.CreateKeyedRepository<int, Person>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void CreateKeyedReadOnlyRepository_Returns_Expected_Type()
        {
            DbSession dbSession = GetSession();

            IKeyedReadOnlyRepository<int, Person> repository = dbSession.CreateKeyedReadOnlyRepository<int, Person>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void Dispose_Is_Implemented()
        {
            DbSession dbSession = GetSession();
            dbSession.Dispose();
        }

        [TestMethod]
        public void Commit_Is_Implemented()
        {
            DbSession dbSession = GetSession();
            dbSession.Commit();
        }

        private static DbSession GetSession()
        {
            InMemoryDb db = new InMemoryDb();

            DbSession dbSession = new DbSession(db);

            return dbSession;
        }
    }
}