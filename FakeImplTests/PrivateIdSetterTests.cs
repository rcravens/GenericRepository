using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.FakeImpl;

namespace FakeImplTests
{
    [TestClass]
    public class PrivateIdSetterTests
    {
        public class TestObj
        {
            public string Id { get; private set;}
            public int PrivateSetter { get; private set; }
            public Guid PublicSetter { get; set;}
        }

        [TestMethod]
        public void IsKeyPrivate_Defaults_To_Expected_Convention()
        {
            PrivateKeySetter setter = new PrivateKeySetter();

            TestObj test = new TestObj();

            Assert.IsTrue(setter.IsKeyPrivate(test));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsKeyPrivate_Throws_If_Property_Not_Found()
        {
            PrivateKeySetter setter = new PrivateKeySetter();

            TestObj test = new TestObj();

            setter.IsKeyPrivate(test, "NotFound");
        }

        [TestMethod]
        public void IsKeyPrivate_Returns_True()
        {
            PrivateKeySetter setter = new PrivateKeySetter();

            TestObj test = new TestObj();

            Assert.IsTrue(setter.IsKeyPrivate(test, "PrivateSetter"));
        }

        [TestMethod]
        public void IsKeyPrivate_Returns_False()
        {
            PrivateKeySetter setter = new PrivateKeySetter();

            TestObj test = new TestObj();

            Assert.IsFalse(setter.IsKeyPrivate(test, "PublicSetter"));
        }

        [TestMethod]
        public void SetId_Defaults_To_Expected_Convention()
        {
            PrivateKeySetter setter = new PrivateKeySetter();

            TestObj test = new TestObj();
            const string idValue = "10";

            Assert.IsTrue(setter.SetKey(test, idValue));
            Assert.IsTrue(test.PrivateSetter == 0);
            Assert.IsTrue(test.PublicSetter == Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetId_Throws_If_Property_Not_Found()
        {
            PrivateKeySetter setter = new PrivateKeySetter();

            TestObj test = new TestObj();
            const int idValue = 10;

            setter.SetKey(test, "Not Found", idValue);
        }

        [TestMethod]
        [ExpectedException(typeof(SettingsPropertyWrongTypeException))]
        public void SetId_Throws_If_Property_Type_Not_Same_As_Value()
        {
            PrivateKeySetter setter = new PrivateKeySetter();

            TestObj test = new TestObj();
            const int idValue = 10;

            setter.SetKey(test, "PublicSetter", idValue);
        }

        [TestMethod]
        public void SetId_Returns_True_And_Sets_Property_With_Private_Setter()
        {
            PrivateKeySetter setter = new PrivateKeySetter();

            TestObj test = new TestObj();
            const int idValue = 10;

            Assert.IsTrue(setter.SetKey(test, "PrivateSetter", idValue));
            Assert.IsTrue(test.Id == null);
            Assert.IsTrue(test.PrivateSetter == idValue);
            Assert.IsTrue(test.PublicSetter == Guid.Empty);
        }

        [TestMethod]
        public void SetId_Returns_True_And_Sets_Property_With_Public_Setter()
        {
            PrivateKeySetter setter = new PrivateKeySetter();

            TestObj test = new TestObj();
            Guid idValue = Guid.NewGuid();

            Assert.IsTrue(setter.SetKey(test, "PublicSetter", idValue));
            Assert.IsTrue(test.Id == null);
            Assert.IsTrue(test.PrivateSetter == 0);
            Assert.IsTrue(test.PublicSetter == idValue);
        }
    }
}