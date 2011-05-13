Generic Repository is a generic repository implementation using .NET 
=================================================================================================

The intention is to provide a *generic repository implementation* for many of the common ORMs. This provides a 'normalized' interface
to the persistence layer. This 'normalization' has costs and benefits.

#### Costs
+ Hides useful features of the ORM
+ Adds complexity to the design

#### Benefits
+ Abstracts away the ORM / persistence implementation
+ Allows the persistence layer to be faked for testing

The following implementatons exist (others are on their way):

+ In Memory (this is useful for automated testing)
+ NHibernate

*If you want to supply an implementation for an ORM please let me know.*


Example Usage
-----------------------

    // Create an instance of the session factory. This is typically done
	//	once and cached. DbSessionFactory is a concrete implementation
	//	of IDbSessionFactory using a variety of ORMs (e.g. NHibernate,
	//	EF...etc).
	//
    IDbSessionFactory dbSessionFactory = new DbSessionFactory(connectionString)
    
	// When necessary...
	//
	// Create a session. This represents a database transaction.
	//
	using( IDbSesseion session = dbSessionFactory.Create())
	{
		// Create a repository.
		//
		IKeyedRepository<Guid, Person> repo = session.CreateKeyedRepository<Guid, Person>();
		
		// Perform actions on the repository
		//
		Person person = new Person {Id = Guid.NewGuid(), FirstName = "Bob", LastName = "Cravens" };
		repo.Add(person);
	
	    // Commit the transaction.
		//
	    session.Commit();
	}


More details will follow. 

Bob