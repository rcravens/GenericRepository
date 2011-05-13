using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.NHibernateImpl;

namespace NHibernateImplTests
{
    [TestClass]
    public class NHibernateHelperTests
    {
        private readonly DbSessionFactory _dbSessionFactory = new DbSessionFactory(Helpers.ConnectionString);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_Excpection_If_Connection_String_Is_Null()
        {
            new NHibernateHelper(null);
        }
    }
}