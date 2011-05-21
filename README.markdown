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
+ Entity Framework

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


Getting Started
----------------------
To get started do the following:

1. Pull down the code from Github. At some point I will put togther Nuget packages to make this step easier.
2. Read the code in the <code>Infrastructure</code> project. These interfaces are the abstraction that you will use in your code. Recommended order: 
    + <code>IDbSessionFactory</code>
	+ <code>IDbSession</code>
    + <code>Repository</code>
    + <code>IReadOnlyRepository</code>
	+ <code>IKeyed</code>
	+ <code>IKeyedRepository</code>
	+ <code>IKeyedReadOnlyRepository</code>
3. Choose one of the existing sample implementations and read that code. If you are familiar with that ORM, then this will be a farily straight-forward process of understanding how it maps into the generic interfaces.
4. Adapt one of the existing implementation for your specific needs.

Feedback
--------------
If you have improvements, questions or suggestions please feel free to contact me.


Bob