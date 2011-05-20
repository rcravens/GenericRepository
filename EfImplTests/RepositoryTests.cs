using System;
using System.Collections.Generic;
using System.Linq;
using EfImpl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Infrastructure;

namespace EfImplTests
{
    [TestClass]
    public class RepositoryTests
    {
        private readonly List<Person> _persons = new List<Person>();
        private readonly DbSessionFactory _dbSessionFactory = new DbSessionFactory(Helpers.ConnectionString);

        [TestInitialize]
        public void Init()
        {
            // before each test
            AddTestData();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // after each test
            CleanUp();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_Exceptions_If_Session_Is_Null()
        {
            new Repository<Person>(null);
        }

        [TestMethod]
        public void Create_Adds_Entities_To_The_Table()
        {
            int currentCount;
            const int numToAdd = 3;
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                // create
                currentCount = repo.All().Count();
                for (int i = 0; i < numToAdd; i++)
                {
                    Assert.IsTrue(repo.Add(CreatePerson()));
                }

                dbSession.Commit();
            }
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();
                int newCount = repo.All().Count();

                Assert.IsTrue(currentCount + numToAdd == newCount);
            }
        }

        [TestMethod]
        public void Create_Adds_A_List_Of_Entities_To_The_Table()
        {
            int startCount;
            const int numToAdd = 3;

            List<Person> localList = new List<Person>();
            for (int i = 0; i < numToAdd; i++)
            {
                localList.Add(CreatePerson());
            }

            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                // create
                startCount = repo.All().Count();
                Assert.IsTrue(repo.Add(localList));

                dbSession.Commit();
            }

            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();
                int finalCount = repo.All().Count();

                Assert.IsTrue(finalCount == startCount + numToAdd);
            }
        }

        [TestMethod]
        public void Read_Finds_Exising()
        {
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                foreach (Person person in _persons)
                {
                    Person copy = repo.FindBy(person.Id);
                    Assert.IsNotNull(copy);
                    Assert.IsTrue(copy .Equals(person));
                }

                dbSession.Commit();
            }
        }

        [TestMethod]
        public void Read_Returns_Null_If_Id_Does_Not_Exist()
        {
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                Person shouldBeNull = repo.FindBy(Guid.NewGuid());
                Assert.IsNull(shouldBeNull);

                dbSession.Commit();
            }
        }

        [TestMethod]
        public void Update_Returns_True_And_Modifies_Exising()
        {
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                int countBefore = repo.All().Count();
                Person person1Updated = repo.FindBy(_persons[0].Id);
                person1Updated.LastName = Guid.NewGuid().ToString();
                Assert.IsTrue(repo.Update(person1Updated));
                Assert.AreNotEqual(person1Updated, _persons[0]);
                Assert.IsTrue(repo.All().Count() == countBefore);

                dbSession.Commit();
            }
        }

        [TestMethod]
        public void Update_Returns_False_If_Does_Not_Exist_And_Does_Not_Modify_List()
        {
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                int countBefore = repo.All().Count();
                Person doesNotExist = new Person { FirstName = "Julio", LastName = "Gonzalas" };
                Assert.IsFalse(repo.Update(doesNotExist));
                Assert.IsTrue(repo.All().Count() == countBefore);

                dbSession.Commit();
            }
        }

        [TestMethod]
        public void All_Returns_Expected()
        {
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                List<Person> all = repo.All().ToList();
                Assert.IsTrue(all.Count >= _persons.Count);
                foreach (Person person in _persons)
                {
                    Assert.IsTrue(all.IndexOf(person) != -1);
                }

                dbSession.Commit();
            }
        }

        [TestMethod]
        public void Delete_Returns_False_If_Does_Not_Exist_And_Does_Not_Modify_List()
        {
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                int countBefore = repo.All().Count();
                Person doesNotExist = new Person { FirstName = "I do not exist", LastName = "I do not exist" };
                Assert.IsFalse(repo.Delete(doesNotExist));
                Assert.IsTrue(repo.All().Count() == countBefore);
            }
        }

        [TestMethod]
        public void Delete_Returns_True_And_Removes_Entity_If_Found()
        {
            int expectedCount;
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                expectedCount = repo.All().Count() - _persons.Count;
                foreach (Person person in _persons)
                {
                    Assert.IsTrue(repo.Delete(person));
                }

                dbSession.Commit();
            }
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();
                Assert.IsTrue(repo.All().Count() == expectedCount);
            }
        }

        [TestMethod]
        public void Delete_Returns_True_And_Removes_A_List_Of_Entities_If_Found()
        {
            int expected;
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                expected = repo.All().Count() - _persons.Count;
                Assert.IsTrue(repo.Delete(_persons));

                dbSession.Commit();
            }
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                Assert.IsTrue(repo.All().Count() == expected);
            }
        }

        private Person CreatePerson()
        {
            Person person = new Person
                                {
                                    Id = Guid.NewGuid(),
                                    FirstName = Guid.NewGuid().ToString(), 
                                    LastName = Guid.NewGuid().ToString()
                                };

            // track it for deletion
            _persons.Add(person);
            return person;
        }

        private void AddTestData()
        {
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                for (int i = 0; i < 5; i++)
                {
                    repo.Add(CreatePerson());
                }

                dbSession.Commit();
            }
        }

        private void CleanUp()
        {
            using (IDbSessionGuidKeyed dbSession = _dbSessionFactory.Create())
            {
                IGuidKeyedRepository<Person> repo = dbSession.CreateKeyedRepository<Person>();

                // delete what we created
                foreach (Person person in _persons)
                {
                    repo.Delete(person);
                }

                dbSession.Commit();
            }
        }
    }
}