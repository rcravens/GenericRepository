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
            new Repository<Guid, Person>(null);
        }

        [TestMethod]
        public void Create_Adds_Entities_To_The_Table()
        {
            int currentCount;
            const int numToAdd = 3;
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                // create
                currentCount = repo.All().Count();
                for (int i = 0; i < numToAdd; i++)
                {
                    Assert.IsTrue(repo.Add(CreatePerson()));
                }

                dbSession.Commit();
            }
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();
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

            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                // create
                startCount = repo.All().Count();
                Assert.IsTrue(repo.Add(localList));

                dbSession.Commit();
            }

            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();
                int finalCount = repo.All().Count();

                Assert.IsTrue(finalCount == startCount + numToAdd);
            }
        }

        [TestMethod]
        public void Read_Finds_Exising()
        {
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                foreach (Person person in _persons)
                {
                    Person copy = repo.FindBy(person.Id);
                    Assert.IsNotNull(copy);
                    Assert.AreEqual(copy, person);
                }

                dbSession.Commit();
            }
        }

        [TestMethod]
        public void Read_Returns_Null_If_Id_Does_Not_Exist()
        {
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                Person shouldBeNull = repo.FindBy(Guid.NewGuid());
                Assert.IsNull(shouldBeNull);

                dbSession.Commit();
            }
        }

        [TestMethod]
        public void Update_Returns_True_And_Modifies_Exising()
        {
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

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
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

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
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

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
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                int countBefore = repo.All().Count();
                Person doesNotExist = new Person { FirstName = "I do not exist", LastName = "I do not exist" };
                Assert.IsFalse(repo.Delete(doesNotExist));
                Assert.IsTrue(repo.All().Count() == countBefore);
            }
        }

        [TestMethod]
        public void Delete_Returns_True_And_Removes_Entity_If_Found()
        {
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                int expectedCount = repo.All().Count();
                foreach (Person person in _persons)
                {
                    Assert.IsTrue(repo.All().Count() == expectedCount--);
                    Assert.IsTrue(repo.Delete(person));
                    Assert.IsTrue(repo.All().Count() == expectedCount);
                }
            }
        }

        [TestMethod]
        public void Delete_Returns_True_And_Removes_A_List_Of_Entities_If_Found()
        {
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                int expected = repo.All().Count() - _persons.Count;
                Assert.IsTrue(repo.Delete(_persons));
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
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                for (int i = 0; i < 5; i++)
                {
                    Assert.IsTrue(repo.Add(CreatePerson()));
                }

                dbSession.Commit();
            }
        }

        private void CleanUp()
        {
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<Guid, Person> repo = dbSession.CreateKeyedRepository<Guid, Person>();

                // delete what we created
                foreach (Person person in _persons)
                {
                    Assert.IsTrue(repo.Delete(person));
                }

                dbSession.Commit();
            }
        }
    }
}