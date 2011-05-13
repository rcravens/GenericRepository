using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.FakeImpl;

namespace FakeImplTests
{
    [TestClass]
    public class FakeRepositoryTests
    {
        //private readonly List<Person> _persons = new List<Person>
        //    { 
        //        new Person { Id = 0, Name = "Bob Cravens" },
        //        new Person { Id = 1, Name = "John Smith" },
        //        new Person { Id = 2, Name = "Sarah Black" }
        //    };

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_Exceptions_If_Table_Is_Null()
        {
            //new FakeRepository<int, Person>(null);

            Maps maps = Maps.Deserialize();

            int xxx = 0;
        }
/*
        [TestMethod]
        public void Create_Adds_Entities_To_The_Table()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            FakeRepository<int, Person> repo = new FakeRepository<int, Person>(table);

            // create
            int expectedCount = 0;
            foreach (Person person in _persons)
            {
                Assert.IsTrue(table.Count() == expectedCount++);
                Assert.IsTrue(repo.Add(person));
                Assert.IsTrue(table.Count() == expectedCount);
            }
        }

        [TestMethod]
        public void Create_Adds_A_List_Of_Entities_To_The_Table()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            FakeRepository<int, Person> repo = new FakeRepository<int, Person>(table);

            // create
            Assert.IsTrue(repo.Add(_persons));
            Assert.IsTrue(table.Count() == _persons.Count);
            Assert.IsTrue(repo.Count() == _persons.Count);
            Assert.IsTrue(repo.All().Count() == _persons.Count);
        }

        [TestMethod]
        public void Create_Fails_For_Existing_Ids()
        {
            FakeRepository<int, Person> repo = PopulateRepo();

            // will not add with same id
            Person bad = new Person { Id = 1, Name = "I already exist" };
            Assert.IsFalse(repo.Add(bad));
            Assert.IsTrue(repo.Count() == 3);
        }

        [TestMethod]
        public void Read_Finds_Exising()
        {
            FakeRepository<int, Person> repo = PopulateRepo();

            foreach (Person person in _persons)
            {
                Person copy = repo.FindBy(person.Id);
                Assert.IsNotNull(copy);
                Assert.AreEqual(copy, person);
            }
        }

        [TestMethod]
        public void Read_Returns_Null_If_Id_Does_Not_Exist()
        {
            FakeRepository<int, Person> repo = PopulateRepo();

            Person shouldBeNull = repo.FindBy(100);
            Assert.IsNull(shouldBeNull);
        }

        [TestMethod]
        public void Update_Returns_True_And_Modifies_Exising()
        {
            FakeRepository<int, Person> repo = PopulateRepo();

            Person person1Updated = new Person { Id = 1, Name = "Julio Gonzalas" };
            Assert.IsTrue(repo.Update(person1Updated));
            Assert.AreNotEqual(person1Updated, _persons[1]);
            Assert.IsTrue(repo.Count() == _persons.Count);
        }

        [TestMethod]
        public void Update_Returns_False_If_Does_Not_Exist_And_Does_Not_Modify_List()
        {
            FakeRepository<int, Person> repo = PopulateRepo();

            Person doesNotExist = new Person { Id = 100, Name = "Julio Gonzalas" };
            Assert.IsFalse(repo.Update(doesNotExist));
            Assert.IsTrue(repo.Count() == _persons.Count);
            foreach (Person person in _persons)
            {
                Assert.AreEqual(person, _persons[person.Id]);
            }
        }

        [TestMethod]
        public void All_Returns_Expected()
        {
            FakeRepository<int, Person> repo = PopulateRepo();

            List<Person> all = repo.All().ToList();
            Assert.IsTrue(all.Count == 3);
            Assert.IsTrue(all.IndexOf(_persons[0]) == 0);
            Assert.IsTrue(all.IndexOf(_persons[1]) == 1);
            Assert.IsTrue(all.IndexOf(_persons[2]) == 2);
        }

        [TestMethod]
        public void Delete_Returns_False_If_Does_Not_Exist_And_Does_Not_Modify_List()
        {
            FakeRepository<int, Person> repo = PopulateRepo();

            Person doesNotExist = new Person {Id = 100, Name = "I do not exist"};
            Assert.IsFalse(repo.Delete(doesNotExist));
            Assert.IsTrue(repo.Count() == _persons.Count);
            foreach (Person person in _persons)
            {
                Assert.AreEqual(person, _persons[person.Id]);
            }
        }

        [TestMethod]
        public void Delete_Returns_True_And_Removes_Entity_If_Found()
        {
            FakeRepository<int, Person> repo = PopulateRepo();

            int expectedCount = 3;
            foreach (Person person in _persons)
            {
                Assert.IsTrue(repo.Count() == expectedCount--);
                Assert.IsTrue(repo.Delete(person));
                Assert.IsTrue(repo.Count() == expectedCount);
            }
        }

        [TestMethod]
        public void Delete_Returns_True_And_Removes_A_List_Of_Entities_If_Found()
        {
            FakeRepository<int, Person> repo = PopulateRepo();

            Assert.IsTrue(repo.Delete(_persons));
            Assert.IsTrue(repo.Count() == 0);
        }

        private FakeRepository<int, Person> PopulateRepo()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            FakeRepository<int, Person> repo = new FakeRepository<int, Person>(table);

            foreach (Person person in _persons)
            {
                repo.Add(person);
            }

            return repo;
        }*/
    }
}