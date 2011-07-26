This is an implementation of the generic database / repository infrastrcuture. By
using this implementation you encapsulate the implementation details of the 
database and the ORM. This encapsulation allows the database to be faked for
testing purposes. This solution contains a fake db implementation in a separate
project. To complete this NHibernate implementation:

Simply create a configuration file for NHibernate and add it to your project.

The constructor of the IDbSessionFactory implementation takes an Assembly
that contains the NHibernate mapping resources. Look at the tests for an
example.