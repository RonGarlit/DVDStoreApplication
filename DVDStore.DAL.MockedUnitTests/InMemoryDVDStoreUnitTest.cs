using Microsoft.EntityFrameworkCore;
using System.Globalization;

/**********************************************************************************
**
**  DVDStore.DAL.MockedUnitTests v1.0
**
**  Copyright 2024
**  Developed by:
**     Ronald Garlit.
**
**  This software is released into the public domain for training purposes and use
**  for presentations to members of the Southwest Florida Coders User group.
**
**  Use is subject to license terms.
***********************************************************************************
**
**  FileName: InMemoryDVDStoreUnitTest.cs (DVDStore.DAL.MockedUnitTests)
**  Version: 1.0
**  Author: Ronald Garlit
**
**  Description:
**  This database provider allows Entity Framework Core to be used with an in-memory
**  database. While some users use the in-memory database for testing, this is
**  NORMALLY discouraged. Our DAL design and DbContext used in he DVDStore.DAL is
**  specifically designed for the way we test allowing for integration with our MVC
**  Controllers and other classes.
**
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-03-15		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

namespace DVDStore.DAL.MockedUnitTests
{
    [TestClass]
    public class InMemoryDVDStoreUnitTest
    {
        #region Private Fields

        // In-memory database options for the DbContext
        private DbContextOptions<DVDStoreDbContext>? _options;

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        public void Test001_CreateActor()
        {
            // Arrange
            using var context = new DVDStoreDbContext(_options);
            var actor = new Actor { Actorid = 11, Firstname = "RONALD", Lastname = "GARLIT", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) };

            // Act
            context.Actors.Add(actor);
            context.SaveChanges();

            // Assert
            var retrievedActor = context.Actors.FirstOrDefault(a => a.Actorid == actor.Actorid);
            Assert.IsNotNull(retrievedActor, "Retrieval of Actor added is not found in the database.");

            // Additional asserts
            Assert.AreEqual(actor.Actorid, retrievedActor.Actorid, "Actor IDs do not match.");
            Assert.AreEqual(actor.Firstname, retrievedActor.Firstname, "First names do not match.");
            Assert.AreEqual(actor.Lastname, retrievedActor.Lastname, "Last names do not match.");
            Assert.AreEqual(actor.Lastupdate, retrievedActor.Lastupdate, "Last update dates do not match.");
        }

        [TestMethod]
        public void Test002_UpdateActor()
        {
            // Arrange
            using var context = new DVDStoreDbContext(_options);
            var actor = context.Actors.FirstOrDefault(); // Get an existing actor

            if (actor != null)
            {
                // Act
                actor.Firstname = "UpdatedFirstName";
                context.SaveChanges();

                // Assert
                var updatedActor = context.Actors.Find(actor.Actorid);
                Assert.IsNotNull(updatedActor, "Updated actor not found in the database.");
                Assert.AreEqual("UpdatedFirstName", updatedActor!.Firstname, "Firstname did not update correctly.");

                // Additional assertions
                Assert.AreEqual(actor.Actorid, updatedActor!.Actorid, "Actor IDs do not match.");
                Assert.AreEqual(actor.Lastname, updatedActor!.Lastname, "Last names do not match.");
                Assert.AreEqual(actor.Lastupdate, updatedActor!.Lastupdate, "Last update dates do not match.");

                // Add additional assertions as needed
            }
            else
            {
                Assert.Fail("No actor found to update");
            }
        }

        [TestMethod]
        public void Test003_DeleteActor()
        {
            // Arrange
            using var context = new DVDStoreDbContext(_options);
            var actor = context.Actors.FirstOrDefault(); // Get an existing actor

            // Act
            if (actor != null)
            {
                context.Actors.Remove(actor);
                context.SaveChanges();
            }
            else
            {
                Assert.Fail("No actor found to delete");
            }

            // Assert
            var deletedActor = context.Actors.Find(actor?.Actorid);
            Assert.IsNull(deletedActor, "Deleted actor still exists in the database.");

            // Additional assertions
            if (actor != null)
            {
                Assert.IsFalse(context.Actors.Any(a => a.Actorid == actor.Actorid), $"Actor with ID {actor.Actorid} is still present in the database.");
            }
        }

        [TestMethod]
        public void Test004_ReadActors()
        {
            // Arrange
            using var context = new DVDStoreDbContext(_options);
            // Act
            var actors = context.Actors.ToList();

            // Assert
            Assert.IsNotNull(actors, "The list of actors is null.");

            // Assert the count of actors
            Assert.AreEqual(10, actors.Count, "The count of actors in the list is not as expected.");

            // Assert specific actor values
            var betteNicholson = actors.Find(a => a.Actorid == 6);
            Assert.IsNotNull(betteNicholson, "Actor Bette Nicholson not found in the list.");

            Assert.AreEqual(6, betteNicholson.Actorid, "Actor ID is not as expected.");
            Assert.AreEqual("BETTE", betteNicholson.Firstname, "Actor firstname is not as expected.");
            Assert.AreEqual("NICHOLSON", betteNicholson.Lastname, "Actor lastname is not as expected.");
            Assert.AreEqual(new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local), betteNicholson.Lastupdate, "Actor last update is not as expected.");
        }

        [TestMethod]
        public void Test005_CreateFilm()
        {
            // Arrange
            using var context = new DVDStoreDbContext(_options);
            var film = new Film { Filmid = 11, Title = "New Film", Description = "Description of new film", Releaseyear = "2024", Languageid = 1, Rentalduration = 5, Rentalrate = 3.99m, Length = 120, Replacementcost = 19.99m, Rating = "PG-13", Specialfeatures = "Trailers", Lastupdate = DateTime.Now };

            // Act
            context.Films.Add(film);
            context.SaveChanges();

            // Assert
            var retrievedFilm = context.Films.FirstOrDefault(f => f.Filmid == film.Filmid);
            Assert.IsNotNull(retrievedFilm, "Retrieved film is null.");

            Assert.AreEqual(film.Filmid, retrievedFilm.Filmid, "Film ID is not as expected.");
            Assert.AreEqual(film.Title, retrievedFilm.Title, "Film title is not as expected.");
            Assert.AreEqual(film.Description, retrievedFilm.Description, "Film description is not as expected.");
            Assert.AreEqual(film.Releaseyear, retrievedFilm.Releaseyear, "Film release year is not as expected.");
            Assert.AreEqual(film.Languageid, retrievedFilm.Languageid, "Film language ID is not as expected.");
            Assert.AreEqual(film.Rentalduration, retrievedFilm.Rentalduration, "Film rental duration is not as expected.");
            Assert.AreEqual(film.Rentalrate, retrievedFilm.Rentalrate, "Film rental rate is not as expected.");
            Assert.AreEqual(film.Length, retrievedFilm.Length, "Film length is not as expected.");
            Assert.AreEqual(film.Replacementcost, retrievedFilm.Replacementcost, "Film replacement cost is not as expected.");
            Assert.AreEqual(film.Rating, retrievedFilm.Rating, "Film rating is not as expected.");
            Assert.AreEqual(film.Specialfeatures, retrievedFilm.Specialfeatures, "Film special features are not as expected.");
            Assert.AreEqual(film.Lastupdate, retrievedFilm.Lastupdate, "Film last update is not as expected.");

            // Count assertion
            var filmCount = context.Films.Count();
            Assert.AreEqual(11, filmCount, "Film count is not as expected.");
        }

        [TestMethod]
        public void Test006_UpdateFilm()
        {
            // Arrange
            using var context = new DVDStoreDbContext(_options);
            var film = context.Films.FirstOrDefault(); // Get an existing film

            // Act
            if (film != null)
            {
                film.Title = "Updated Film Title";
                context.SaveChanges();

                // Assert
                var updatedFilm = context.Films.Find(film.Filmid);
                Assert.IsNotNull(updatedFilm, "Updated film is null.");
                Assert.AreEqual("Updated Film Title", updatedFilm!.Title, "Film title is not as expected.");

                // Add additional assertions as needed
                Assert.AreEqual(film.Description, updatedFilm!.Description, "Film description is not as expected.");
                Assert.AreEqual(film.Releaseyear, updatedFilm!.Releaseyear, "Film release year is not as expected.");
                // Add assertions for other properties similarly
            }
            else
            {
                Assert.Fail("No film found to update.");
            }
        }

        [TestMethod]
        public void Test007_DeleteFilm()
        {
            // Arrange
            using var context = new DVDStoreDbContext(_options);
            var film = context.Films.FirstOrDefault(); // Get an existing film

            // Act
            if (film != null)
            {
                context.Films.Remove(film);
                context.SaveChanges();

                // Assert
                var deletedFilm = context.Films.Find(film.Filmid);
                Assert.IsNull(deletedFilm, "Deleted film is not null.");
            }
            else
            {
                Assert.Fail("No film found to delete");
            }
        }

        [TestMethod]
        public void Test008_ReadFilms()
        {
            // Arrange
            using var context = new DVDStoreDbContext(_options);
            // Act
            var films = context.Films.ToList();
            var expectedDate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture);

            // Assert
            Assert.IsNotNull(films, "Films list is not null.");

            // Additional assertions for record count
            Assert.AreEqual(10, films.Count, "Film count is not as expected.");

            // Find and assert values for specific film
            var specificFilm = films.Find(f => f.Filmid == 7);
            Assert.IsNotNull(specificFilm, "Film with ID 7 is found.");

            // Assert specific values in the film
            Assert.AreEqual(expected: "AIRPLANE SIERRA", actual: specificFilm!.Title, message: "Film title is not as expected.");
            Assert.AreEqual(expected: "A Touching Saga of a Hunter And a Butler who must Discover a Butler in A Jet Boat", actual: specificFilm!.Description, message: "Film description is not as expected.");
            Assert.AreEqual(expected: "2006", actual: specificFilm!.Releaseyear, message: "Release year is not as expected.");
            Assert.AreEqual(expected: (byte)1, actual: specificFilm!.Languageid, message: "Language ID is not as expected.");
            Assert.AreEqual(expected: (byte)6, actual: specificFilm!.Rentalduration, message: "Rental duration is not as expected.");
            Assert.AreEqual(expected: 4.99m, actual: specificFilm!.Rentalrate, message: "Rental rate is not as expected.");
            Assert.AreEqual(expected: (short)62, actual: specificFilm!.Length, message: "Film length is not as expected.");
            Assert.AreEqual(expected: 28.99m, actual: specificFilm!.Replacementcost, message: "Replacement cost is not as expected.");
            Assert.AreEqual(expected: "PG-13", actual: specificFilm!.Rating, message: "Rating is not as expected.");
            Assert.AreEqual(expected: "Trailers,Deleted Scenes", actual: specificFilm!.Specialfeatures, message: "Special features are not as expected.");
            Assert.AreEqual(expected: expectedDate, actual: specificFilm!.Lastupdate, message: "Last update time is not as expected.");
        }

        [TestMethod]
        public void Test009_CreateCustomer()
        {
            // Arrange
            using var context = new DVDStoreDbContext(_options);
            var customer = new Customer { Customerid = 11, Storeid = 1, Firstname = "Cindi", Lastname = "Garlit", Email = "Cindi.Garlit@example.com", Addressid = 1, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) };

            // Act
            context.Customers.Add(customer);
            context.SaveChanges();

            // Assert
            var retrievedCustomer = context.Customers.FirstOrDefault(c => c.Customerid == customer.Customerid);
            Assert.IsNotNull(retrievedCustomer, "Retrieved customer is not null.");

            // Additional assertions for expected values
            Assert.AreEqual(customer.Customerid, retrievedCustomer.Customerid, "Customer ID is not as expected.");
            Assert.AreEqual(customer.Storeid, retrievedCustomer.Storeid, "Store ID is not as expected.");
            Assert.AreEqual(customer.Firstname, retrievedCustomer.Firstname, "First name is not as expected.");
            Assert.AreEqual(customer.Lastname, retrievedCustomer.Lastname, "Last name is not as expected.");
            Assert.AreEqual(customer.Email, retrievedCustomer.Email, "Email is not as expected.");
            Assert.AreEqual(customer.Addressid, retrievedCustomer.Addressid, "Address ID is not as expected.");
            Assert.AreEqual(customer.Active, retrievedCustomer.Active, "Active status is not as expected.");
            Assert.AreEqual(customer.Createdate, retrievedCustomer.Createdate, "Create date is not as expected.");
            Assert.AreEqual(customer.Lastupdate, retrievedCustomer.Lastupdate, "Last update is not as expected.");

            // Count assertion
            var customerCount = context.Customers.Count();
            Assert.AreEqual(11, customerCount, "Customer count is not as expected.");
        }

        [TestMethod]
        public void Test010_UpdateCustomer()
        {
            // Arrange
            using var context = new DVDStoreDbContext(_options);
            var customer = context.Customers.FirstOrDefault(); // Get an existing customer

            // Act
            if (customer != null)
            {
                customer.Firstname = "UpdatedFirstName";
                context.SaveChanges();

                // Assert
                var updatedCustomer = context.Customers.Find(customer.Customerid);
                Assert.IsNotNull(updatedCustomer, "Updated customer is not null.");

                // Additional assertions for expected values
                Assert.AreEqual("UpdatedFirstName", updatedCustomer!.Firstname, "First name is not as expected.");

                // Check and assert record count
                var customerCount = context.Customers.Count();
                Assert.AreEqual(10, customerCount, "Customer count is not as expected.");

                // Find and assert values for specific customer
                var specificCustomer = context.Customers.FirstOrDefault(c => c.Customerid == customer.Customerid);
                Assert.IsNotNull(specificCustomer, "Specific customer is found.");
                Assert.AreEqual("UpdatedFirstName", specificCustomer!.Firstname, "Specific customer's first name is not as expected.");
                Assert.AreEqual(customer.Lastname, specificCustomer!.Lastname, "Specific customer's last name is not as expected.");
                Assert.AreEqual(customer.Email, specificCustomer!.Email, "Specific customer's email is not as expected.");
                Assert.AreEqual(customer.Addressid, specificCustomer!.Addressid, "Specific customer's address ID is not as expected.");
                Assert.AreEqual(customer.Active, specificCustomer!.Active, "Specific customer's active status is not as expected.");
                Assert.AreEqual(customer.Createdate, specificCustomer!.Createdate, "Specific customer's create date is not as expected.");
                Assert.AreEqual(customer.Lastupdate, specificCustomer!.Lastupdate, "Specific customer's last update is not as expected.");
            }
            else
            {
                Assert.Fail("No customer found to update");
            }
        }

        [TestMethod]
        public void Test011_DeleteCustomer()
        {
            // Arrange
            using var context = new DVDStoreDbContext(_options);
            var customer = context.Customers.FirstOrDefault(); // Get an existing customer

            // Act
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();

                // Assert
                var deletedCustomer = context.Customers.Find(customer.Customerid);
                Assert.IsNull(deletedCustomer, "Deleted customer is null.");

                // Check and assert record count
                var customerCount = context.Customers.Count();
                Assert.AreEqual(9, customerCount, "Customer count is not as expected.");

                // Find and assert values for specific customer
                var specificCustomer = context.Customers.FirstOrDefault(c => c.Customerid == customer.Customerid);
                Assert.IsNull(specificCustomer, "Specific customer is not found.");
            }
            else
            {
                Assert.Fail("No customer found to delete");
            }
        }

        [TestMethod]
        public void Test012_ReadCustomers()
        {
            // Arrange
            using var context = new DVDStoreDbContext(_options);
            // Act
            var customers = context.Customers.ToList();
            var expectedCreateDateString = "2006-02-14 22:04:36";
            var expectedCreateDate = DateTime.Parse(expectedCreateDateString, CultureInfo.InvariantCulture); // Using InvariantCulture for ISO 8601 format
            var expectedLastUpdateDateString = "2006-02-15 04:57:20";
            var expectedLastUpdateDate = DateTime.Parse(expectedLastUpdateDateString, CultureInfo.InvariantCulture); // Using InvariantCulture for ISO 8601 format

            // Assert
            Assert.IsNotNull(customers, "Customers list is not null.");

            // Add additional assertions for record count
            Assert.AreEqual(10, customers.Count, "Customer count is not as expected.");

            // Find and assert specific customer
            var specificCustomer = customers.Find(c => c.Customerid == 8);
            Assert.IsNotNull(specificCustomer, "Customer with ID 8 is found.");

            // Assert specific values in the customer
            Assert.AreEqual("SUSAN", specificCustomer!.Firstname, "Customer firstname is not as expected.");
            Assert.AreEqual("WILSON", specificCustomer!.Lastname, "Customer lastname is not as expected.");
            Assert.AreEqual("SUSAN.WILSON@DVDStorecustomer.org", specificCustomer!.Email, "Customer email is not as expected.");
            Assert.AreEqual(2, specificCustomer!.Storeid, "Customer store ID is not as expected.");
            Assert.AreEqual(12, specificCustomer!.Addressid, "Customer address ID is not as expected.");
            Assert.AreEqual("Y", specificCustomer!.Active, "Customer active status is not as expected.");
            Assert.AreEqual(expectedCreateDate, specificCustomer!.Createdate, "Customer created date is not as expected.");
            Assert.AreEqual(expectedLastUpdateDate, specificCustomer!.Lastupdate, "Customer last update date is not as expected.");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Create a new instance of the DbContext options for each test
            // This ensures that each test has a clean and isolated database to work
            // with and avoids any cross-test interference
            _options = new DbContextOptionsBuilder<DVDStoreDbContext>()
            // Use the in-memory database for this test and configure it to be created with the default settings
            // The database is created for each test method and is destroyed when the test method is finished
                .UseInMemoryDatabase(databaseName: "Test_DVDStore")
                .Options;

            // Seed the in-memory database with test data
            using var context = new DVDStoreDbContext(_options);
            // Add test data to the in-memory database
            context.Actors.AddRange(GetActorList());
            context.Films.AddRange(GetFilmList());
            context.Customers.AddRange(GetCustomerList());
            context.SaveChanges();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Clean up the in-memory database after each test
            using var context = new DVDStoreDbContext(_options);
            context.Database.EnsureDeleted();
        }

        #endregion Public Methods

        #region Private Methods

        private static List<Actor> GetActorList()
        {
            return
            [
                new() { Actorid = 1, Firstname = "PENELOPE", Lastname = "GUINESS", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new() { Actorid = 2, Firstname = "NICK", Lastname = "WAHLBERG", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new() { Actorid = 3, Firstname = "ED", Lastname = "CHASE", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new() { Actorid = 4, Firstname = "JENNIFER", Lastname = "DAVIS", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new() { Actorid = 5, Firstname = "JOHNNY", Lastname = "LOLLOBRIGIDA", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new() { Actorid = 6, Firstname = "BETTE", Lastname = "NICHOLSON", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new() { Actorid = 7, Firstname = "GRACE", Lastname = "MOSTEL", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new() { Actorid = 8, Firstname = "MATTHEW", Lastname = "JOHANSSON", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new() { Actorid = 9, Firstname = "JOE", Lastname = "SWANK", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new() { Actorid = 10, Firstname = "CHRISTIAN", Lastname = "GABLE", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)}
            ];
        }

        private static List<Customer> GetCustomerList()
        {
            // Create a list of customers
            return
            [
                new() { Customerid = 1, Storeid = 1, Firstname = "MARY", Lastname = "SMITH", Email = "MARY.SMITH@DVDStorecustomer.org", Addressid = 5, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new() { Customerid = 2, Storeid = 1, Firstname = "PATRICIA", Lastname = "JOHNSON", Email = "PATRICIA.JOHNSON@DVDStorecustomer.org", Addressid = 6, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new() { Customerid = 3, Storeid = 1, Firstname = "LINDA", Lastname = "WILLIAMS", Email = "LINDA.WILLIAMS@DVDStorecustomer.org", Addressid = 7, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new() { Customerid = 4, Storeid = 2, Firstname = "BARBARA", Lastname = "JONES", Email = "BARBARA.JONES@DVDStorecustomer.org", Addressid = 8, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new() { Customerid = 5, Storeid = 1, Firstname = "ELIZABETH", Lastname = "BROWN", Email = "ELIZABETH.BROWN@DVDStorecustomer.org", Addressid = 9, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new() { Customerid = 6, Storeid = 2, Firstname = "JENNIFER", Lastname = "DAVIS", Email = "JENNIFER.DAVIS@DVDStorecustomer.org", Addressid = 10, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new() { Customerid = 7, Storeid = 1, Firstname = "MARIA", Lastname = "MILLER", Email = "MARIA.MILLER@DVDStorecustomer.org", Addressid = 11, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new() { Customerid = 8, Storeid = 2, Firstname = "SUSAN", Lastname = "WILSON", Email = "SUSAN.WILSON@DVDStorecustomer.org", Addressid = 12, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new() { Customerid = 9, Storeid = 2, Firstname = "MARGARET", Lastname = "MOORE", Email = "MARGARET.MOORE@DVDStorecustomer.org", Addressid = 13, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new() { Customerid = 10, Storeid = 1, Firstname = "DOROTHY", Lastname = "TAYLOR", Email = "DOROTHY.TAYLOR@DVDStorecustomer.org", Addressid = 14, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) }
            ];
        }

        private static List<Film> GetFilmList()
        {
            // Create a list of films
            return
            [
                new() { Filmid = 1, Title = "ACADEMY DINOSAUR", Description = "A Epic Drama of a Feminist And a Mad Scientist who must Battle a Teacher in The Canadian Rockies", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 0.99m, Length = 86, Replacementcost = 20.99m, Rating = "PG", Specialfeatures = "Deleted Scenes,Behind the Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000",CultureInfo.InvariantCulture) },
                new() { Filmid = 2, Title = "ACE GOLDFINGER", Description = "A Astounding Epistle of a Database Administrator And a Explorer who must Find a Car in Ancient China", Releaseyear = "2006", Languageid = 1, Rentalduration = 3, Rentalrate = 4.99m, Length = 48, Replacementcost = 12.99m, Rating = "G", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000",CultureInfo.InvariantCulture) },
                new() { Filmid = 3, Title = "ADAPTATION HOLES", Description = "A Astounding Reflection of a Lumberjack And a Car who must Sink a Lumberjack in A Baloon Factory", Releaseyear = "2006", Languageid = 1, Rentalduration = 7, Rentalrate = 2.99m, Length = 50, Replacementcost = 18.99m, Rating = "NC-17", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new() { Filmid = 4, Title = "AFFAIR PREJUDICE", Description = "A Fanciful Documentary of a Frisbee And a Lumberjack who must Chase a Monkey in A Shark Tank", Releaseyear = "2006", Languageid = 1, Rentalduration = 5, Rentalrate = 2.99m, Length = 117, Replacementcost = 26.99m, Rating = "G", Specialfeatures = "Commentaries,Behind the Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new() { Filmid = 5, Title = "AFRICAN EGG", Description = "A Fast-Paced Documentary of a Pastry Chef And a Dentist who must Pursue a Forensic Psychologist in The Gulf of Mexico", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 2.99m, Length = 130, Replacementcost = 22.99m, Rating = "G", Specialfeatures = "Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new() { Filmid = 6, Title = "AGENT TRUMAN", Description = "A Intrepid Panorama of a Robot And a Boy who must Escape a Sumo Wrestler in Ancient China", Releaseyear = "2006", Languageid = 1, Rentalduration = 3, Rentalrate = 2.99m, Length = 169, Replacementcost = 17.99m, Rating = "PG", Specialfeatures = "Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new() { Filmid = 7, Title = "AIRPLANE SIERRA", Description = "A Touching Saga of a Hunter And a Butler who must Discover a Butler in A Jet Boat", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 4.99m, Length = 62, Replacementcost = 28.99m, Rating = "PG-13", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new() { Filmid = 8, Title = "AIRPORT POLLOCK", Description = "A Epic Tale of a Moose And a Girl who must Confront a Monkey in Ancient India", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 4.99m, Length = 54, Replacementcost = 15.99m, Rating = "R", Specialfeatures = "Trailers", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new() { Filmid = 9, Title = "ALABAMA DEVIL", Description = "A Thoughtful Panorama of a Database Administrator And a Mad Scientist who must Outgun a Mad Scientist in A Jet Boat", Releaseyear = "2006", Languageid = 1, Rentalduration = 3, Rentalrate = 2.99m, Length = 114, Replacementcost = 21.99m, Rating = "PG-13", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new() { Filmid = 10, Title = "ALADDIN CALENDAR", Description = "A Action-Packed Tale of a Man And a Lumberjack who must Reach a Feminist in Ancient China", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 4.99m, Length = 63, Replacementcost = 24.99m, Rating = "NC-17", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture  ) }
            ];
        }

        #endregion Private Methods
    }
}