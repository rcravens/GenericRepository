using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.FakeImpl;

namespace FakeImplTests
{
    [TestClass]
    public class InMemoryDbTableTests
    {/*
        private readonly List<Person> _persons = new List<Person>
            { 
                new Person { Id = 0, Name = "Bob Cravens" },
                new Person { Id = 1, Name = "John Smith" },
                new Person { Id = 2, Name = "Sarah Black" }
            };

        [TestMethod]
        public void Create_Adds_New_Entities()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> personTable = db.GetTable<int, Person>();

            // create
            int expectedCount = 0;
            foreach (Person person in _persons)
            {
                Assert.IsTrue(personTable.Count() == expectedCount++);
                Assert.IsTrue(personTable.Add(person));
                Assert.IsTrue(personTable.Count() == expectedCount);
            }
        }

        [TestMethod]
        public void Create_Fails_For_Existing_Ids()
        {
            InMemoryDbTable<int, Person> personTable = PopulateTable();

            // will not add with same id
            Person bad = new Person { Id = 1, Name = "I already exist" };
            Assert.IsFalse(personTable.Add(bad));
            Assert.IsTrue(personTable.Count() == 3);
        }

        [TestMethod]
        public void Read_Finds_Exising()
        {
            InMemoryDbTable<int, Person> personTable = PopulateTable();

            foreach (Person person in _persons)
            {
                Person copy = personTable.FindBy(person.Id);
                Assert.IsNotNull(copy);
                Assert.AreEqual(copy, person);
            }
        }

        [TestMethod]
        public void Read_Returns_Null_If_Id_Does_Not_Exist()
        {
            InMemoryDbTable<int, Person> personTable = PopulateTable();

            Person shouldBeNull = personTable.FindBy(100);
            Assert.IsNull(shouldBeNull);
        }

        [TestMethod]
        public void Update_Returns_True_And_Modifies_Exising()
        {
            InMemoryDbTable<int, Person> personTable = PopulateTable();

            Person person1Updated = new Person { Id = 1, Name = "Julio Gonzalas" };
            Assert.IsTrue(personTable.Update(person1Updated));
            Assert.AreNotEqual(person1Updated, _persons[1]);
            Assert.IsTrue(personTable.Count() == _persons.Count);
        }

        [TestMethod]
        public void Update_Returns_False_If_Does_Not_Exist_And_Does_Not_Modify_List()
        {
            InMemoryDbTable<int, Person> personTable = PopulateTable();

            Person doesNotExist = new Person { Id = 100, Name = "Julio Gonzalas" };
            Assert.IsFalse(personTable.Update(doesNotExist));
            Assert.IsTrue(personTable.Count() == _persons.Count);
            foreach (Person person in _persons)
            {
                Assert.AreEqual(person, _persons[person.Id]);
            }
        }

        [TestMethod]
        public void All_Returns_Expected()
        {
            InMemoryDbTable<int, Person> personTable = PopulateTable();

            List<Person> all = personTable.All().ToList();
            Assert.IsTrue(all.Count == 3);
            Assert.IsTrue(all.IndexOf(_persons[0]) == 0);
            Assert.IsTrue(all.IndexOf(_persons[1]) == 1);
            Assert.IsTrue(all.IndexOf(_persons[2]) == 2);
        }

        [TestMethod]
        public void Delete_Returns_False_If_Does_Not_Exist_And_Does_Not_Modify_List()
        {
            InMemoryDbTable<int, Person> personTable = PopulateTable();

            Assert.IsFalse(personTable.Delete(100));
            Assert.IsTrue(personTable.Count() == _persons.Count);
            foreach (Person person in _persons)
            {
                Assert.AreEqual(person, _persons[person.Id]);
            }
        }

        [TestMethod]
        public void Delete_Returns_True_And_Removes_Entity_If_Found()
        {
            InMemoryDbTable<int, Person> personTable = PopulateTable();

            int expectedCount = 3;
            foreach (Person person in _persons)
            {
                Assert.IsTrue(personTable.Count() == expectedCount--);
                Assert.IsTrue(personTable.Delete(person.Id));
                Assert.IsTrue(personTable.Count() == expectedCount);
            }
        }

        private InMemoryDbTable<int, Person> PopulateTable()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> personTable = db.GetTable<int, Person>();

            foreach (Person person in _persons)
            {
                personTable.Add(person);
            }

            return personTable;
        }*/
    }
}