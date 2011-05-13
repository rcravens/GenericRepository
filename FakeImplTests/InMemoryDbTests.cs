using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.DataModel.Dtos;
using Repository.FakeImpl;

namespace FakeImplTests
{
    [TestClass]
    public class InMemoryDbTests
    {
        [TestMethod]
        public void GetTable_CreatesNewTables()
        {
            InMemoryDb db = new InMemoryDb();

            Assert.IsTrue(db.TableCount() == 0);

            InMemoryDbTable<int, Person> people = db.GetTable<int, Person>();
            Assert.IsNotNull(people);
            Assert.IsTrue(db.TableCount() == 1);

            InMemoryDbTable<int, Address> addresses = db.GetTable<int, Address>();
            Assert.IsNotNull(addresses);
            Assert.IsTrue(db.TableCount() == 2);
        }

        [TestMethod]
        public void GetTable_ReturnsExistingTable()
        {
            InMemoryDb db = new InMemoryDb();

            Assert.IsTrue(db.TableCount() == 0);

            InMemoryDbTable<int, Person> people = db.GetTable<int, Person>();
            Assert.IsNotNull(people);
            Assert.IsTrue(people.Count() == 0);
            Assert.IsTrue(db.TableCount() == 1);

            InMemoryDbTable<int, Address> address = db.GetTable<int, Address>();
            Assert.IsNotNull(address);
            Assert.IsTrue(db.TableCount() == 2);

            Person person = new Person {FirstName = "first name", LastName = "last name"};
            people.Add(person);
            Assert.IsTrue(people.Count() == 1);

            InMemoryDbTable<int, Person> peopleCopy = db.GetTable<int, Person>();
            Assert.IsNotNull(peopleCopy);
            Assert.IsTrue(peopleCopy.Count() == 1);
        }
    }
}