using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.DataModel.Dtos;
using Repository.Infrastructure;
using Repository.NHibernateImpl;

namespace NHibernateImplTests
{
    [TestClass]
    public class DbSessionTests
    {
        private readonly DbSessionFactory _dbSessionFactory = new DbSessionFactory(Helpers.ConnectionString);

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
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                dbSession.CreateReadOnlyRepository<Person>();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CreateRepository_Throws_Exception()
        {
            using(IDbSession dbSession = _dbSessionFactory.Create())
            {
                dbSession.CreateRepository<Person>();                
            }
        }

        [TestMethod]
        public void CreateKeyedRepository_Returns_Expected_Type()
        {
            using(IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<int, Person> repository = dbSession.CreateKeyedRepository<int, Person>();
                Assert.IsNotNull(repository);                
            }
        }

        [TestMethod]
        public void CreateKeyedReadOnlyRepository_Returns_Expected_Type()
        {
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedReadOnlyRepository<int, Person> repository = dbSession.CreateKeyedReadOnlyRepository<int, Person>();
                Assert.IsNotNull(repository);
            }

        }

    }
}