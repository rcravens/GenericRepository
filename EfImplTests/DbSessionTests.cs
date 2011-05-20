using System;
using EfImpl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Infrastructure;

namespace EfImplTests
{
    [TestClass]
    public class DbSessionTests
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
        public void Rollback_Throws_Exception()
        {
            DbSession dbSession = GetSession();

            throw new NotImplementedException("Testing rollback needs to be done after repo is working");
            dbSession.Rollback();
        }

        [TestMethod]
        public void CreateKeyedRepository_Returns_Expected_Type()
        {
            DbSession dbSession = GetSession();

            IKeyedRepository<Guid, Person> repository = dbSession.CreateKeyedRepository<Guid, Person>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void CreateKeyedReadOnlyRepository_Returns_Expected_Type()
        {
            DbSession dbSession = GetSession();

            IKeyedReadOnlyRepository<Guid, Person> repository = dbSession.CreateKeyedReadOnlyRepository<Guid, Person>();
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
            db2Entities context = new db2Entities(Helpers.ConnectionString);
            return new DbSession(context);
        }
    }
}