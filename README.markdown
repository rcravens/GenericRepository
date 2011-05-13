Generic Repository is a generic repository framework using .NET 
=================================================================================================

The intention is to provide a *generic repository implementation* for many of the common ORMs. More details will follow. The following
implementatons exist (others are on their way):

+ In Memory (this is useful for automated testing)
+ NHibernate

Example Usage
-------------

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


If you want so supply an implementation for an ORM please let me know.

Bob