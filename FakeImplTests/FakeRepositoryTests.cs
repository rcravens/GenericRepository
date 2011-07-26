using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.DataModel.Dtos;
using Repository.FakeImpl;
using Repository.DataModel;
using Repository.Infrastructure;

namespace FakeImplTests
{
    [TestClass]
    public class FakeRepositoryTests
    {
        public class TestObj : IKeyed<int>
        {
            public int Id { get; set;}
        }

        public class GuidKey : IKeyed<Guid>
        {
            public Guid Id { get; private set; }
        }

        public class StringKey : IKeyed<String>
        {
            public String Id { get; private set; }
        }

        public class DateTimeKey : IKeyed<DateTime>
        {
            public DateTime Id { get; private set; }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_Exceptions_If_Table_Is_Null()
        {
            new Repository<int, Person>(null);
        }

        [TestMethod]
        public void Create_Adds_Entities_To_The_Table_And_Sets_Int_Key()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            Repository<int, Person> repo = new Repository<int, Person>(table);
            List<Person> people = CreateRandomList();

            // create
            int expectedCount = 0;
            foreach (Person person in people)
            {
                Assert.IsTrue(table.Count() == expectedCount++);
                Assert.IsTrue(repo.Add(person));
                Assert.IsTrue(table.Count() == expectedCount);
                Assert.IsTrue(person.Id == expectedCount - 1);
            }
        }

        [TestMethod]
        public void Create_Adds_Entity_To_The_Table_And_Sets_Guid_Key()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<Guid, GuidKey> table = db.GetTable<Guid, GuidKey>();
            Repository<Guid, GuidKey> repo = new Repository<Guid, GuidKey>(table);
            
            GuidKey entity = new GuidKey();

            // create
            Assert.IsTrue(repo.Add(entity));
            Assert.IsTrue(table.Count() == 1);
            Assert.IsTrue(entity.Id != Guid.Empty);
        }

        [TestMethod]
        public void Create_Adds_Entity_To_The_Table_And_Sets_String_Key()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<string, StringKey> table = db.GetTable<string, StringKey>();
            Repository<string, StringKey> repo = new Repository<string, StringKey>(table);

            StringKey entity = new StringKey();

            // create
            Assert.IsTrue(repo.Add(entity));
            Assert.IsTrue(table.Count() == 1);
            Assert.IsTrue(entity.Id != null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Create_Throws_If_Key_Type_Not_Supported()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<DateTime, DateTimeKey> table = db.GetTable<DateTime, DateTimeKey>();
            Repository<DateTime, DateTimeKey> repo = new Repository<DateTime, DateTimeKey>(table);

            DateTimeKey entity = new DateTimeKey();

            // create
            repo.Add(entity);
        }

        [TestMethod]
        public void Create_Adds_A_List_Of_Entities_To_The_Table()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            Repository<int, Person> repo = new Repository<int, Person>(table);
            List<Person> people = CreateRandomList();

            // create
            Assert.IsTrue(repo.Add(people));
            Assert.IsTrue(table.Count() == people.Count);
            Assert.IsTrue(repo.Count() == people.Count);
            Assert.IsTrue(repo.All().Count() == people.Count);
        }

        [TestMethod]
        public void Create_Fails_For_Existing_Ids()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, TestObj> table = db.GetTable<int, TestObj>();
            Repository<int, TestObj> repo = new Repository<int, TestObj>(table);

            TestObj obj1 = new TestObj {Id = 3};
            Assert.IsTrue(repo.Add(obj1));

            TestObj obj2 = new TestObj {Id = 3}; // Oops...same ID

            // will not add with same id
            Assert.IsFalse(repo.Add(obj2));
            Assert.IsTrue(repo.Count() == 1);
        }

        [TestMethod]
        public void Read_Finds_Exising()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            Repository<int, Person> repo = new Repository<int, Person>(table);

            List<Person> people = CreateRandomList();
            repo.Add(people);

            foreach (Person person in people)
            {
                Person copy = repo.FindBy(person.Id);
                Assert.IsNotNull(copy);
                Assert.AreEqual(copy, person);
            }
        }

        [TestMethod]
        public void Read_Returns_Null_If_Id_Does_Not_Exist()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            Repository<int, Person> repo = new Repository<int, Person>(table);

            List<Person> people = CreateRandomList();
            repo.Add(people);

            Person shouldBeNull = repo.FindBy(100);
            Assert.IsNull(shouldBeNull);
        }

        [TestMethod]
        public void Update_Returns_True_And_Modifies_Existing()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            Repository<int, Person> repo = new Repository<int, Person>(table);

            List<Person> people = CreateRandomList();
            repo.Add(people);

            // Create a new 'detached' person.
            //
            Person person1Updated = new Person
                                        {
                                            FirstName = Guid.NewGuid().ToString(),
                                            LastName = Guid.NewGuid().ToString()
                                        };
            PrivateKeySetter privateKeySetter = new PrivateKeySetter();
            privateKeySetter.SetKey(person1Updated, people[0].Id);


            Assert.IsTrue(repo.Update(person1Updated));
            Assert.AreNotEqual(person1Updated, people[0]);
            Assert.IsTrue(repo.Count() == people.Count);
        }

        [TestMethod]
        public void Update_Returns_False_If_Does_Not_Exist_And_Does_Not_Modify_List()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            Repository<int, Person> repo = new Repository<int, Person>(table);

            List<Person> people = CreateRandomList();
            repo.Add(people);

            // Create a new 'detached' person that does not exist.
            //
            Person doesNotExist = new Person
                                        {
                                            FirstName = Guid.NewGuid().ToString(),
                                            LastName = Guid.NewGuid().ToString()
                                        };
            PrivateKeySetter privateKeySetter = new PrivateKeySetter();
            privateKeySetter.SetKey(doesNotExist, 100);

            Assert.IsFalse(repo.Update(doesNotExist));
            Assert.IsTrue(repo.Count() == people.Count);
            foreach (Person person in people)
            {
                Assert.AreEqual(person, people[person.Id]);
            }
        }

        [TestMethod]
        public void All_Returns_Expected()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            Repository<int, Person> repo = new Repository<int, Person>(table);

            List<Person> people = CreateRandomList();
            repo.Add(people);

            List<Person> all = repo.All().ToList();
            Assert.IsTrue(all.Count == people.Count);
            foreach (Person person in all)
            {
                Person found = people.Where(x => x.Id == person.Id).FirstOrDefault();
                Assert.IsNotNull(found);
                Assert.AreEqual(found, person);
            }
        }

        [TestMethod]
        public void Delete_Returns_False_If_Does_Not_Exist_And_Does_Not_Modify_List()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            Repository<int, Person> repo = new Repository<int, Person>(table);

            List<Person> people = CreateRandomList();
            repo.Add(people);


            // Create a new 'detached' person that does not exist.
            //
            Person doesNotExist = new Person
                                        {
                                            FirstName = Guid.NewGuid().ToString(),
                                            LastName = Guid.NewGuid().ToString()
                                        };
            PrivateKeySetter privateKeySetter = new PrivateKeySetter();
            privateKeySetter.SetKey(doesNotExist, 100);

            Assert.IsFalse(repo.Delete(doesNotExist));
            Assert.IsTrue(repo.Count() == people.Count);
            foreach (Person person in people)
            {
                Assert.AreEqual(person, people[person.Id]);
            }
        }

        [TestMethod]
        public void Delete_Returns_True_And_Removes_Entity_If_Found()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            Repository<int, Person> repo = new Repository<int, Person>(table);

            List<Person> people = CreateRandomList();
            repo.Add(people);

            int expectedCount = people.Count;
            foreach (Person person in people)
            {
                Assert.IsTrue(repo.Count() == expectedCount--);
                Assert.IsTrue(repo.Delete(person));
                Assert.IsTrue(repo.Count() == expectedCount);
            }
        }

        [TestMethod]
        public void Delete_Returns_True_And_Removes_A_List_Of_Entities_If_Found()
        {
            InMemoryDb db = new InMemoryDb();
            InMemoryDbTable<int, Person> table = db.GetTable<int, Person>();
            Repository<int, Person> repo = new Repository<int, Person>(table);

            List<Person> people = CreateRandomList();
            repo.Add(people);

            Assert.IsTrue(repo.Delete(people));
            Assert.IsTrue(repo.Count() == 0);
        }

        private readonly Random _random = new Random(DateTime.Now.Millisecond);

        private List<Person> CreateRandomList()
        {
            List<Person> people = new List<Person>();

            int numPeople = _random.Next(3, 10);
            for (int i = 0; i < numPeople; i++)
            {
                Person person = new Person
                {
                    Id = i,
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                };
                people.Add(person);
            }

            return people;
        }
    }
}