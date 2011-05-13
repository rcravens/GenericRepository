using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.DataModel.Dtos;
using Repository.Infrastructure;
using Repository.NHibernateImpl;

namespace NHibernateImplTests
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
            new Repository<int, Person>(null);
        }

        [TestMethod]
        public void Create_Adds_Entities_To_The_Table()
        {
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<int, Person> repo = dbSession.CreateKeyedRepository<int, Person>();

                // create
                int expectedCount = repo.All().Count();
                for (int i = 0; i < 3; i++)
                {
                    Assert.IsTrue(repo.All().Count() == expectedCount++);
                    Assert.IsTrue(repo.Add(CreatePerson()));
                    Assert.IsTrue(repo.All().Count() == expectedCount);
                }

                dbSession.Commit();
            }
        }

        [TestMethod]
        public void Create_Adds_A_List_Of_Entities_To_The_Table()
        {

            List<Person> localList = new List<Person>();
            for (int i = 0; i < 3; i++)
            {
                localList.Add(CreatePerson());
            }

            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<int, Person> repo = dbSession.CreateKeyedRepository<int, Person>();

                // create
                int countBefore = repo.All().Count();
                Assert.IsTrue(repo.Add(localList));
                Assert.IsTrue(repo.All().Count() == countBefore + localList.Count);

                dbSession.Commit();
            }
        }

        [TestMethod]
        public void Read_Finds_Exising()
        {
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<int, Person> repo = dbSession.CreateKeyedRepository<int, Person>();

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
                IKeyedRepository<int, Person> repo = dbSession.CreateKeyedRepository<int, Person>();

                Person shouldBeNull = repo.FindBy(int.MaxValue-1);
                Assert.IsNull(shouldBeNull);

                dbSession.Commit();
            }
        }

        [TestMethod]
        public void Update_Returns_True_And_Modifies_Exising()
        {
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<int, Person> repo = dbSession.CreateKeyedRepository<int, Person>();

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
                IKeyedRepository<int, Person> repo = dbSession.CreateKeyedRepository<int, Person>();

                int countBefore = repo.All().Count();
                Person doesNotExist = new Person {FirstName = "Julio", LastName = "Gonzalas"};
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
                IKeyedRepository<int, Person> repo = dbSession.CreateKeyedRepository<int, Person>();

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
                IKeyedRepository<int, Person> repo = dbSession.CreateKeyedRepository<int, Person>();

                int countBefore = repo.All().Count();
                Person doesNotExist = new Person {FirstName = "I do not exist", LastName = "I do not exist"};
                Assert.IsFalse(repo.Delete(doesNotExist));
                Assert.IsTrue(repo.All().Count() == countBefore);
            }
        }

        [TestMethod]
        public void Delete_Returns_True_And_Removes_Entity_If_Found()
        {
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<int, Person> repo = dbSession.CreateKeyedRepository<int, Person>();

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
                IKeyedRepository<int, Person> repo = dbSession.CreateKeyedRepository<int, Person>();

                int expected = repo.All().Count() - _persons.Count;
                Assert.IsTrue(repo.Delete(_persons));
                Assert.IsTrue(repo.All().Count() == expected);
            }
        }

        private Person CreatePerson()
        {
            Person person = new Person {FirstName = Guid.NewGuid().ToString(), LastName = Guid.NewGuid().ToString()};
            
            // track it for deletion
            _persons.Add(person);
            return person;
        }

        private void AddTestData()
        {
            using (IDbSession dbSession = _dbSessionFactory.Create())
            {
                IKeyedRepository<int, Person> repo = dbSession.CreateKeyedRepository<int, Person>();

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
                IKeyedRepository<int, Person> repo = dbSession.CreateKeyedRepository<int, Person>();

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