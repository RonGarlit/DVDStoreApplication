Use this folder for a custom repository.

This is used to centralize and handle many things related to the database.

This bulls the Custom DAL which can be faked/mocked with data for testing and development purposes when the database isn't ready or in flux.

This is where we handle database paging, filtering, searching and data shaping functions.

Not only is data access centralized in these repositories.

But the advantage of this design allows minimization of data being exposes and sent over the wire.

It also improves application performance and safety. 