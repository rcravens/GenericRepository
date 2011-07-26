using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.NHibernateImpl;

namespace NHibernateImplTests
{
    [TestClass]
    public class NHibernateHelperTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_Excpection_If_Connection_String_Is_Null()
        {
            new NHibernateHelper(null, Helpers.ResourceAssembly);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_Excpection_If_Resource_Assembly_Is_Null()
        {
            new NHibernateHelper("connection string", null);
        }
    }
}