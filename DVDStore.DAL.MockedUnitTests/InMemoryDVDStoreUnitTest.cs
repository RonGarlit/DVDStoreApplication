using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DVDStore.DAL.MockedUnitTests
{
    [TestClass]
    public class InMemoryDVDStoreUnitTest
    {
        #region Private Fields

        private DbContextOptions<DVDStoreDbContext>? _options;

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        public void Test001_CreateActor()
        {
            // Arrange
            using (var context = new DVDStoreDbContext(_options))
            {
                var actor = new Actor { Actorid = 11, Firstname = "RONALD", Lastname = "GARLIT", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20) };

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
        }

        [TestMethod]
        public void Test002_UpdateActor()
        {
            // Arrange
            using (var context = new DVDStoreDbContext(_options))
            {
                var actor = context.Actors.FirstOrDefault(); // Get an existing actor

                if (actor != null)
                {
                    // Act
                    actor.Firstname = "UpdatedFirstName";
                    context.SaveChanges();

                    // Assert
                    var updatedActor = context.Actors.Find(actor.Actorid);
                    Assert.IsNotNull(updatedActor, "Updated actor not found in the database.");
                    Assert.AreEqual("UpdatedFirstName", updatedActor?.Firstname, "Firstname did not update correctly.");

                    // Additional assertions
                    Assert.AreEqual(actor.Actorid, updatedActor?.Actorid, "Actor IDs do not match.");
                    Assert.AreEqual(actor.Lastname, updatedActor?.Lastname, "Last names do not match.");
                    Assert.AreEqual(actor.Lastupdate, updatedActor?.Lastupdate, "Last update dates do not match.");

                    // Add additional assertions as needed
                }
                else
                {
                    Assert.Fail("No actor found to update");
                }
            }
        }

        [TestMethod]
        public void Test003_DeleteActor()
        {
            // Arrange
            using (var context = new DVDStoreDbContext(_options))
            {
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
        }

        [TestMethod]
        public void Test004_ReadActors()
        {
            // Arrange
            using (var context = new DVDStoreDbContext(_options))
            {
                // Act
                var actors = context.Actors.ToList();

                // Assert
                Assert.IsNotNull(actors, "The list of actors is null.");

                // Assert the count of actors
                Assert.AreEqual(10, actors.Count, "The count of actors in the list is not as expected.");

                // Assert specific actor values
                var betteNicholson = actors.FirstOrDefault(a => a.Actorid == 6);
                Assert.IsNotNull(betteNicholson, "Actor Bette Nicholson not found in the list.");

                Assert.AreEqual(6, betteNicholson.Actorid, "Actor ID is not as expected.");
                Assert.AreEqual("BETTE", betteNicholson.Firstname, "Actor firstname is not as expected.");
                Assert.AreEqual("NICHOLSON", betteNicholson.Lastname, "Actor lastname is not as expected.");
                Assert.AreEqual(new DateTime(2006, 02, 15, 04, 57, 20), betteNicholson.Lastupdate, "Actor last update is not as expected.");
            }
        }

        [TestMethod]
        public void Test005_CreateFilm()
        {
            // Arrange
            using (var context = new DVDStoreDbContext(_options))
            {
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
        }

        [TestMethod]
        public void Test006_UpdateFilm()
        {
            // Arrange
            using (var context = new DVDStoreDbContext(_options))
            {
                var film = context.Films.FirstOrDefault(); // Get an existing film

                // Act
                if (film != null)
                {
                    film.Title = "Updated Film Title";
                    context.SaveChanges();

                    // Assert
                    var updatedFilm = context.Films.Find(film.Filmid);
                    Assert.IsNotNull(updatedFilm, "Updated film is null.");
                    Assert.AreEqual("Updated Film Title", updatedFilm?.Title, "Film title is not as expected.");

                    // Add additional assertions as needed
                    Assert.AreEqual(film.Description, updatedFilm?.Description, "Film description is not as expected.");
                    Assert.AreEqual(film.Releaseyear, updatedFilm?.Releaseyear, "Film release year is not as expected.");
                    // Add assertions for other properties similarly
                }
                else
                {
                    Assert.Fail("No film found to update.");
                }
            }
        }

        [TestMethod]
        public void Test007_DeleteFilm()
        {
            // Arrange
            using (var context = new DVDStoreDbContext(_options))
            {
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
        }

        [TestMethod]
        public void Test008_ReadFilms()
        {
            // Arrange
            using (var context = new DVDStoreDbContext(_options))
            {
                // Act
                var films = context.Films.ToList();

                // Assert
                Assert.IsNotNull(films, "Films list is not null.");

                // Additional assertions for record count
                Assert.AreEqual(10, films.Count, "Film count is not as expected.");

                // Find and assert values for specific film
                var specificFilm = films.FirstOrDefault(f => f.Filmid == 7);
                Assert.IsNotNull(specificFilm, "Film with ID 7 is found.");

                // Assert specific values in the film
                Assert.AreEqual("AIRPLANE SIERRA", specificFilm?.Title, "Film title is not as expected.");
                Assert.AreEqual("A Touching Saga of a Hunter And a Butler who must Discover a Butler in A Jet Boat", specificFilm?.Description, "Film description is not as expected.");
                Assert.AreEqual("2006", specificFilm?.Releaseyear, "Release year is not as expected.");
                Assert.AreEqual((byte)1, specificFilm?.Languageid, "Language ID is not as expected.");
                Assert.AreEqual((byte)6, specificFilm?.Rentalduration, "Rental duration is not as expected.");
                Assert.AreEqual(4.99m, specificFilm?.Rentalrate, "Rental rate is not as expected.");
                Assert.AreEqual((short)62, specificFilm?.Length, "Film length is not as expected.");
                Assert.AreEqual(28.99m, specificFilm?.Replacementcost, "Replacement cost is not as expected.");
                Assert.AreEqual("PG-13", specificFilm?.Rating, "Rating is not as expected.");
                Assert.AreEqual("Trailers,Deleted Scenes", specificFilm?.Specialfeatures, "Special features are not as expected.");
                Assert.AreEqual(DateTime.Parse("2006-02-15 05:03:42.000"), specificFilm?.Lastupdate, "Last update time is not as expected.");
            }
        }

        [TestMethod]
        public void Test009_CreateCustomer()
        {
            // Arrange
            using (var context = new DVDStoreDbContext(_options))
            {
                var customer = new Customer { Customerid = 11, Storeid = 1, Firstname = "Cindi", Lastname = "Garlit", Email = "Cindi.Garlit@example.com", Addressid = 1, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20) };

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
        }

        [TestMethod]
        public void Test010_UpdateCustomer()
        {
            // Arrange
            using (var context = new DVDStoreDbContext(_options))
            {
                var customer = context.Customers.FirstOrDefault(); // Get an existing customer

                // Act
                if (customer != null)
                {
                    customer.Firstname = "UpdatedFirstName";
                    context.SaveChanges();

                    // Assert
                    var updatedCustomer = context.Customers.Find(customer.Customerid);
                    Assert.AreEqual("UpdatedFirstName", updatedCustomer?.Firstname);
                    // Add additional assertions as needed
                }
                else
                {
                    Assert.Fail("No customer found to update");
                }
            }
        }

        [TestMethod]
        public void Test011_DeleteCustomer()
        {
            // Arrange
            using (var context = new DVDStoreDbContext(_options))
            {
                var customer = context.Customers.FirstOrDefault(); // Get an existing customer

                // Act
                if (customer != null)
                {
                    context.Customers.Remove(customer);
                    context.SaveChanges();

                    // Assert
                    var deletedCustomer = context.Customers.Find(customer.Customerid);
                    Assert.IsNull(deletedCustomer);
                    // Add additional assertions as needed
                }
                else
                {
                    Assert.Fail("No customer found to delete");
                }
            }
        }

        [TestMethod]
        public void Test012_ReadCustomers()
        {
            // Arrange
            using (var context = new DVDStoreDbContext(_options))
            {
                // Act
                var customers = context.Customers.ToList();

                // Assert
                Assert.IsNotNull(customers);
                // Add additional assertions as needed
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _options = new DbContextOptionsBuilder<DVDStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_DVDStore")
                .Options;

            // Seed the in-memory database with test data
            using (var context = new DVDStoreDbContext(_options))
            {
                // Clear existing data
                context.Actors.RemoveRange(context.Actors);
                context.Films.RemoveRange(context.Films);
                context.Customers.RemoveRange(context.Customers);
                context.SaveChanges();

                context.Actors.AddRange(GetActorList());
                context.Films.AddRange(GetFilmList());
                context.Customers.AddRange(GetCustomerList());
                context.SaveChanges();
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static List<Actor> GetActorList()
        {
            return new List<Actor>
            {
                new Actor { Actorid = 1, Firstname = "PENELOPE", Lastname = "GUINESS", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20)},
                new Actor { Actorid = 2, Firstname = "NICK", Lastname = "WAHLBERG", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20)},
                new Actor { Actorid = 3, Firstname = "ED", Lastname = "CHASE", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20)},
                new Actor { Actorid = 4, Firstname = "JENNIFER", Lastname = "DAVIS", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20)},
                new Actor { Actorid = 5, Firstname = "JOHNNY", Lastname = "LOLLOBRIGIDA", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20)},
                new Actor { Actorid = 6, Firstname = "BETTE", Lastname = "NICHOLSON", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20)},
                new Actor { Actorid = 7, Firstname = "GRACE", Lastname = "MOSTEL", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20)},
                new Actor { Actorid = 8, Firstname = "MATTHEW", Lastname = "JOHANSSON", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20)},
                new Actor { Actorid = 9, Firstname = "JOE", Lastname = "SWANK", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20)},
                new Actor { Actorid = 10, Firstname = "CHRISTIAN", Lastname = "GABLE", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20)}
            };
        }

        private static List<Customer> GetCustomerList()
        {
            // Create a list of customers
            return new List<Customer>
            {
                new Customer { Customerid = 1, Storeid = 1, Firstname = "MARY", Lastname = "SMITH", Email = "MARY.SMITH@DVDStorecustomer.org", Addressid = 5, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20) },
                new Customer { Customerid = 2, Storeid = 1, Firstname = "PATRICIA", Lastname = "JOHNSON", Email = "PATRICIA.JOHNSON@DVDStorecustomer.org", Addressid = 6, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20) },
                new Customer { Customerid = 3, Storeid = 1, Firstname = "LINDA", Lastname = "WILLIAMS", Email = "LINDA.WILLIAMS@DVDStorecustomer.org", Addressid = 7, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20) },
                new Customer { Customerid = 4, Storeid = 2, Firstname = "BARBARA", Lastname = "JONES", Email = "BARBARA.JONES@DVDStorecustomer.org", Addressid = 8, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20) },
                new Customer { Customerid = 5, Storeid = 1, Firstname = "ELIZABETH", Lastname = "BROWN", Email = "ELIZABETH.BROWN@DVDStorecustomer.org", Addressid = 9, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20) },
                new Customer { Customerid = 6, Storeid = 2, Firstname = "JENNIFER", Lastname = "DAVIS", Email = "JENNIFER.DAVIS@DVDStorecustomer.org", Addressid = 10, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20) },
                new Customer { Customerid = 7, Storeid = 1, Firstname = "MARIA", Lastname = "MILLER", Email = "MARIA.MILLER@DVDStorecustomer.org", Addressid = 11, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20) },
                new Customer { Customerid = 8, Storeid = 2, Firstname = "SUSAN", Lastname = "WILSON", Email = "SUSAN.WILSON@DVDStorecustomer.org", Addressid = 12, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20) },
                new Customer { Customerid = 9, Storeid = 2, Firstname = "MARGARET", Lastname = "MOORE", Email = "MARGARET.MOORE@DVDStorecustomer.org", Addressid = 13, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20) },
                new Customer { Customerid = 10, Storeid = 1, Firstname = "DOROTHY", Lastname = "TAYLOR", Email = "DOROTHY.TAYLOR@DVDStorecustomer.org", Addressid = 14, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20) }
            };
        }

        private static List<Film> GetFilmList()
        {
            // Create a list of films
            return new List<Film>
            {
                new Film { Filmid = 1, Title = "ACADEMY DINOSAUR", Description = "A Epic Drama of a Feminist And a Mad Scientist who must Battle a Teacher in The Canadian Rockies", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 0.99m, Length = 86, Replacementcost = 20.99m, Rating = "PG", Specialfeatures = "Deleted Scenes,Behind the Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000") },
                new Film { Filmid = 2, Title = "ACE GOLDFINGER", Description = "A Astounding Epistle of a Database Administrator And a Explorer who must Find a Car in Ancient China", Releaseyear = "2006", Languageid = 1, Rentalduration = 3, Rentalrate = 4.99m, Length = 48, Replacementcost = 12.99m, Rating = "G", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000") },
                new Film { Filmid = 3, Title = "ADAPTATION HOLES", Description = "A Astounding Reflection of a Lumberjack And a Car who must Sink a Lumberjack in A Baloon Factory", Releaseyear = "2006", Languageid = 1, Rentalduration = 7, Rentalrate = 2.99m, Length = 50, Replacementcost = 18.99m, Rating = "NC-17", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000") },
                new Film { Filmid = 4, Title = "AFFAIR PREJUDICE", Description = "A Fanciful Documentary of a Frisbee And a Lumberjack who must Chase a Monkey in A Shark Tank", Releaseyear = "2006", Languageid = 1, Rentalduration = 5, Rentalrate = 2.99m, Length = 117, Replacementcost = 26.99m, Rating = "G", Specialfeatures = "Commentaries,Behind the Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000") },
                new Film { Filmid = 5, Title = "AFRICAN EGG", Description = "A Fast-Paced Documentary of a Pastry Chef And a Dentist who must Pursue a Forensic Psychologist in The Gulf of Mexico", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 2.99m, Length = 130, Replacementcost = 22.99m, Rating = "G", Specialfeatures = "Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000") },
                new Film { Filmid = 6, Title = "AGENT TRUMAN", Description = "A Intrepid Panorama of a Robot And a Boy who must Escape a Sumo Wrestler in Ancient China", Releaseyear = "2006", Languageid = 1, Rentalduration = 3, Rentalrate = 2.99m, Length = 169, Replacementcost = 17.99m, Rating = "PG", Specialfeatures = "Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000") },
                new Film { Filmid = 7, Title = "AIRPLANE SIERRA", Description = "A Touching Saga of a Hunter And a Butler who must Discover a Butler in A Jet Boat", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 4.99m, Length = 62, Replacementcost = 28.99m, Rating = "PG-13", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000") },
                new Film { Filmid = 8, Title = "AIRPORT POLLOCK", Description = "A Epic Tale of a Moose And a Girl who must Confront a Monkey in Ancient India", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 4.99m, Length = 54, Replacementcost = 15.99m, Rating = "R", Specialfeatures = "Trailers", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000") },
                new Film { Filmid = 9, Title = "ALABAMA DEVIL", Description = "A Thoughtful Panorama of a Database Administrator And a Mad Scientist who must Outgun a Mad Scientist in A Jet Boat", Releaseyear = "2006", Languageid = 1, Rentalduration = 3, Rentalrate = 2.99m, Length = 114, Replacementcost = 21.99m, Rating = "PG-13", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000") },
                new Film { Filmid = 10, Title = "ALADDIN CALENDAR", Description = "A Action-Packed Tale of a Man And a Lumberjack who must Reach a Feminist in Ancient China", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 4.99m, Length = 63, Replacementcost = 24.99m, Rating = "NC-17", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000") }
            };
        }

        #endregion Private Methods
    }
}