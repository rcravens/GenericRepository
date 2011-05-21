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
        public void Commit_And_Rollback_Work()
        {
            Person person = new Person
                                {
                                    Id = Guid.NewGuid(),
                                    FirstName = Guid.NewGuid().ToString(),
                                    LastName = Guid.NewGuid().ToString()
                                };

            // Rollback
            using (DbSession dbSession = GetSession())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                repo.Add(person);

                dbSession.Rollback();
            }
            using (DbSession dbSession = GetSession())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                Assert.IsNull(repo.FindBy(person.Id));
            }
            // Commit
            using (DbSession dbSession = GetSession())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                repo.Add(person);

                dbSession.Commit();
            }
            using (DbSession dbSession = GetSession())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                Assert.IsNotNull(repo.FindBy(person.Id));
            }

            // Cleanup
            using (DbSession dbSession = GetSession())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                repo.Delete(person);

                dbSession.Commit();
            }
        }

        [TestMethod]
        public void CreateKeyedRepository_Returns_Expected_Type()
        {
            DbSession dbSession = GetSession();

            IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void CreateKeyedReadOnlyRepository_Returns_Expected_Type()
        {
            DbSession dbSession = GetSession();

            IKeyedReadOnlyRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();
            Assert.IsNotNull(repo);
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