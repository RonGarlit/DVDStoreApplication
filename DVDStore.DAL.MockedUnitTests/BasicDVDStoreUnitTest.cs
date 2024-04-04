// Ensure you are using the correct namespace for EF Core's DbSet
using Microsoft.EntityFrameworkCore;
using Moq;
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
**  FileName: BasicDVDStoreUnitTest.cs (DVDStore.DAL.MockedUnitTests)
**  Version: 1.0
**  Author: Ronald Garlit
**
**  Description: 
**  This class was my starter class during development and uses MOQ for Mocking.
**  
**  
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-03-10		RGARLIT     STARTED DEVELOPMENT 
***********************************************************************************/
namespace DVDStore.DAL.MockedUnitTests
{
    [TestClass]
    public class BasicDVDStoreUnitTest
    {
        #region Private Fields

        // Holds EF DbContext object for the repository
        private IDVDStoreDbContext? _dvdStoreDbContext;

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        public void Test001_Actors_GetCount()
        {
            // Arrange
            // Message	IDE0059	Unnecessary assignment of a value to 'count'
            // Act
            int count = _dvdStoreDbContext?.Actors?.Count() ?? 0;
            // Assert
            Assert.AreEqual(10, count);
        }

        [TestMethod]
        public void Test002_Films_GetCount()
        {
            // Arrange
            // Message	IDE0059	Unnecessary assignment of a value to 'count'
            // Act
            int count = _dvdStoreDbContext?.Films?.Count() ?? 0;
            // Assert
            Assert.AreEqual(10, count);
        }

        [TestMethod]
        public void Test003_Customers_GetCount()
        {
            // Arrange
            // Message	IDE0059	Unnecessary assignment of a value to 'count'
            // Act
            int count = _dvdStoreDbContext?.Customers?.Count() ?? 0;
            // Assert
            Assert.AreEqual(10, count);
        }

        [TestMethod]
        public void Test004_Actor_Exists()
        {
            // Arrange
            var expectedActor = new Actor { Actorid = 6, Firstname = "BETTE", Lastname = "NICHOLSON", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture)};

            // Act
            var actualActor = _dvdStoreDbContext?.Actors.FirstOrDefault(a => a.Actorid == expectedActor.Actorid);

            // Assert
            Assert.IsNotNull(actualActor);
            Assert.AreEqual(expectedActor.Firstname, actualActor.Firstname);
            Assert.AreEqual(expectedActor.Lastname, actualActor.Lastname);
            Assert.AreEqual(expectedActor.Actorid, actualActor.Actorid);
        }

        [TestMethod]
        public void Test005_Film_Exists()
        {
            // Arrange
            var expectedFilm = new Film
            {
                Filmid = 5,
                Title = "AFRICAN EGG",
                Description = "A Fast-Paced Documentary of a Pastry Chef And a Dentist who must Pursue a Forensic Psychologist in The Gulf of Mexico",
                Releaseyear = "2006",
                Languageid = 1,
                Rentalduration = 6,
                Rentalrate = 2.99m,
                Length = 130,
                Replacementcost = 22.99m,
                Rating = "G",
                Specialfeatures = "Deleted Scenes",
                Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture)
            };

            // Act
            var actualFilm = _dvdStoreDbContext?.Films.FirstOrDefault(f => f.Filmid == expectedFilm.Filmid);

            // Assert
            Assert.IsNotNull(actualFilm);
            Assert.AreEqual(expectedFilm.Title, actualFilm.Title);
            Assert.AreEqual(expectedFilm.Description, actualFilm.Description);
            Assert.AreEqual(expectedFilm.Releaseyear, actualFilm.Releaseyear);
            Assert.AreEqual(expectedFilm.Languageid, actualFilm.Languageid);
            Assert.AreEqual(expectedFilm.Rentalduration, actualFilm.Rentalduration);
            Assert.AreEqual(expectedFilm.Rentalrate, actualFilm.Rentalrate);
            Assert.AreEqual(expectedFilm.Length, actualFilm.Length);
            Assert.AreEqual(expectedFilm.Replacementcost, actualFilm.Replacementcost);
            Assert.AreEqual(expectedFilm.Rating, actualFilm.Rating);
            Assert.AreEqual(expectedFilm.Specialfeatures, actualFilm.Specialfeatures);
            Assert.AreEqual(expectedFilm.Lastupdate, actualFilm.Lastupdate);
        }

        [TestMethod]
        public void Test006_Customer_Exists()
        {
            // Arrange
            var expectedCustomer = new Customer
            {
                Customerid = 8,
                Storeid = 2,
                Firstname = "SUSAN",
                Lastname = "WILSON",
                Email = "SUSAN.WILSON@DVDStorecustomer.org",
                Addressid = 12,
                Active = "Y",
                Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local),
                Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)
            };

            // Act
            var actualCustomer = _dvdStoreDbContext?.Customers.FirstOrDefault(c => c.Customerid == expectedCustomer.Customerid);

            // Assert
            Assert.IsNotNull(actualCustomer);
            Assert.AreEqual(expectedCustomer.Firstname, actualCustomer.Firstname);
            Assert.AreEqual(expectedCustomer.Lastname, actualCustomer.Lastname);
            Assert.AreEqual(expectedCustomer.Email, actualCustomer.Email);
            Assert.AreEqual(expectedCustomer.Storeid, actualCustomer.Storeid);
            Assert.AreEqual(expectedCustomer.Addressid, actualCustomer.Addressid);
            Assert.AreEqual(expectedCustomer.Active, actualCustomer.Active);
            Assert.AreEqual(expectedCustomer.Createdate, actualCustomer.Createdate);
            Assert.AreEqual(expectedCustomer.Lastupdate, actualCustomer.Lastupdate);
        }




        [TestInitialize]
        public void TestInitialize()
        {
            // Mock the IDVDStoreDbContext
            var dvdStoreDbContextMock = new Mock<IDVDStoreDbContext>();

            // Create a list of actors
            List<Actor> actors = GetActorList();

            // Create a mock DbSet<Actor>
            var dbSetMock = new Mock<DbSet<Actor>>();
            dbSetMock.As<IQueryable<Actor>>().Setup(m => m.Provider).Returns(actors.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Actor>>().Setup(m => m.Expression).Returns(actors.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Actor>>().Setup(m => m.ElementType).Returns(actors.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Actor>>().Setup(m => m.GetEnumerator()).Returns(actors.AsQueryable().GetEnumerator());
            // Set the mock DbSet<Actor> as the data source for the DbContext
            dvdStoreDbContextMock.Setup(d => d.Actors).Returns(dbSetMock.Object);

            List<Customer> customers = GetCustomerList();

            // Create a mock DbSet<Customer>
            var dbSetMockCusts = new Mock<DbSet<Customer>>();
            dbSetMockCusts.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.AsQueryable().Provider);
            dbSetMockCusts.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.AsQueryable().Expression);
            dbSetMockCusts.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.AsQueryable().ElementType);
            dbSetMockCusts.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.AsQueryable().GetEnumerator());
            // Set the mock DbSet<Customer> as the data source for the DbContext
            dvdStoreDbContextMock.Setup(d => d.Customers).Returns(dbSetMockCusts.Object);

            // Create a list of films
            List<Film> films = GetFilmList();

            // Create a mock DbSet<Film>
            var dbSetMockFilms = new Mock<DbSet<Film>>();
            dbSetMockFilms.As<IQueryable<Film>>().Setup(m => m.Provider).Returns(films.AsQueryable().Provider);
            dbSetMockFilms.As<IQueryable<Film>>().Setup(m => m.Expression).Returns(films.AsQueryable().Expression);
            dbSetMockFilms.As<IQueryable<Film>>().Setup(m => m.ElementType).Returns(films.AsQueryable().ElementType);
            dbSetMockFilms.As<IQueryable<Film>>().Setup(m => m.GetEnumerator()).Returns(films.AsQueryable().GetEnumerator());

            dvdStoreDbContextMock.Setup(d => d.Films).Returns(dbSetMockFilms.Object);

            // Set the mocked DbContext object to the private field
            // This will be used in the tests to access the data
            _dvdStoreDbContext = dvdStoreDbContextMock.Object;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Set the private field to null
            _dvdStoreDbContext = null;
        }

        #endregion Public Methods

        #region Private Methods

        private static List<Actor> GetActorList()
        {
            return
            [
                new Actor { Actorid = 1, Firstname = "PENELOPE", Lastname = "GUINESS", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new Actor { Actorid = 2, Firstname = "NICK", Lastname = "WAHLBERG", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new Actor { Actorid = 3, Firstname = "ED", Lastname = "CHASE", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new Actor { Actorid = 4, Firstname = "JENNIFER", Lastname = "DAVIS", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new Actor { Actorid = 5, Firstname = "JOHNNY", Lastname = "LOLLOBRIGIDA", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new Actor { Actorid = 6, Firstname = "BETTE", Lastname = "NICHOLSON", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new Actor { Actorid = 7, Firstname = "GRACE", Lastname = "MOSTEL", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new Actor { Actorid = 8, Firstname = "MATTHEW", Lastname = "JOHANSSON", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new Actor { Actorid = 9, Firstname = "JOE", Lastname = "SWANK", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)},
                new Actor { Actorid = 10, Firstname = "CHRISTIAN", Lastname = "GABLE", Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local)}
            ];
        }

        private static List<Customer> GetCustomerList()
        {
            // Create a list of customers
            return
            [
                new Customer { Customerid = 1, Storeid = 1, Firstname = "MARY", Lastname = "SMITH", Email = "MARY.SMITH@DVDStorecustomer.org", Addressid = 5, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new Customer { Customerid = 2, Storeid = 1, Firstname = "PATRICIA", Lastname = "JOHNSON", Email = "PATRICIA.JOHNSON@DVDStorecustomer.org", Addressid = 6, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new Customer { Customerid = 3, Storeid = 1, Firstname = "LINDA", Lastname = "WILLIAMS", Email = "LINDA.WILLIAMS@DVDStorecustomer.org", Addressid = 7, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new Customer { Customerid = 4, Storeid = 2, Firstname = "BARBARA", Lastname = "JONES", Email = "BARBARA.JONES@DVDStorecustomer.org", Addressid = 8, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new Customer { Customerid = 5, Storeid = 1, Firstname = "ELIZABETH", Lastname = "BROWN", Email = "ELIZABETH.BROWN@DVDStorecustomer.org", Addressid = 9, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new Customer { Customerid = 6, Storeid = 2, Firstname = "JENNIFER", Lastname = "DAVIS", Email = "JENNIFER.DAVIS@DVDStorecustomer.org", Addressid = 10, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new Customer { Customerid = 7, Storeid = 1, Firstname = "MARIA", Lastname = "MILLER", Email = "MARIA.MILLER@DVDStorecustomer.org", Addressid = 11, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new Customer { Customerid = 8, Storeid = 2, Firstname = "SUSAN", Lastname = "WILSON", Email = "SUSAN.WILSON@DVDStorecustomer.org", Addressid = 12, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new Customer { Customerid = 9, Storeid = 2, Firstname = "MARGARET", Lastname = "MOORE", Email = "MARGARET.MOORE@DVDStorecustomer.org", Addressid = 13, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) },
                new Customer { Customerid = 10, Storeid = 1, Firstname = "DOROTHY", Lastname = "TAYLOR", Email = "DOROTHY.TAYLOR@DVDStorecustomer.org", Addressid = 14, Active = "Y", Createdate = new DateTime(2006, 02, 14, 22, 04, 36, DateTimeKind.Local), Lastupdate = new DateTime(2006, 02, 15, 04, 57, 20, DateTimeKind.Local) }
            ];
        }

        private static List<Film> GetFilmList()
        {
            // Create a list of films
            return
            [
                new Film { Filmid = 1, Title = "ACADEMY DINOSAUR", Description = "A Epic Drama of a Feminist And a Mad Scientist who must Battle a Teacher in The Canadian Rockies", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 0.99m, Length = 86, Replacementcost = 20.99m, Rating = "PG", Specialfeatures = "Deleted Scenes,Behind the Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new Film { Filmid = 2, Title = "ACE GOLDFINGER", Description = "A Astounding Epistle of a Database Administrator And a Explorer who must Find a Car in Ancient China", Releaseyear = "2006", Languageid = 1, Rentalduration = 3, Rentalrate = 4.99m, Length = 48, Replacementcost = 12.99m, Rating = "G", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new Film { Filmid = 3, Title = "ADAPTATION HOLES", Description = "A Astounding Reflection of a Lumberjack And a Car who must Sink a Lumberjack in A Baloon Factory", Releaseyear = "2006", Languageid = 1, Rentalduration = 7, Rentalrate = 2.99m, Length = 50, Replacementcost = 18.99m, Rating = "NC-17", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new Film { Filmid = 4, Title = "AFFAIR PREJUDICE", Description = "A Fanciful Documentary of a Frisbee And a Lumberjack who must Chase a Monkey in A Shark Tank", Releaseyear = "2006", Languageid = 1, Rentalduration = 5, Rentalrate = 2.99m, Length = 117, Replacementcost = 26.99m, Rating = "G", Specialfeatures = "Commentaries,Behind the Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new Film { Filmid = 5, Title = "AFRICAN EGG", Description = "A Fast-Paced Documentary of a Pastry Chef And a Dentist who must Pursue a Forensic Psychologist in The Gulf of Mexico", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 2.99m, Length = 130, Replacementcost = 22.99m, Rating = "G", Specialfeatures = "Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new Film { Filmid = 6, Title = "AGENT TRUMAN", Description = "A Intrepid Panorama of a Robot And a Boy who must Escape a Sumo Wrestler in Ancient China", Releaseyear = "2006", Languageid = 1, Rentalduration = 3, Rentalrate = 2.99m, Length = 169, Replacementcost = 17.99m, Rating = "PG", Specialfeatures = "Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new Film { Filmid = 7, Title = "AIRPLANE SIERRA", Description = "A Touching Saga of a Hunter And a Butler who must Discover a Butler in A Jet Boat", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 4.99m, Length = 62, Replacementcost = 28.99m, Rating = "PG-13", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new Film { Filmid = 8, Title = "AIRPORT POLLOCK", Description = "A Epic Tale of a Moose And a Girl who must Confront a Monkey in Ancient India", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 4.99m, Length = 54, Replacementcost = 15.99m, Rating = "R", Specialfeatures = "Trailers", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new Film { Filmid = 9, Title = "ALABAMA DEVIL", Description = "A Thoughtful Panorama of a Database Administrator And a Mad Scientist who must Outgun a Mad Scientist in A Jet Boat", Releaseyear = "2006", Languageid = 1, Rentalduration = 3, Rentalrate = 2.99m, Length = 114, Replacementcost = 21.99m, Rating = "PG-13", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) },
                new Film { Filmid = 10, Title = "ALADDIN CALENDAR", Description = "A Action-Packed Tale of a Man And a Lumberjack who must Reach a Feminist in Ancient China", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 4.99m, Length = 63, Replacementcost = 24.99m, Rating = "NC-17", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000", CultureInfo.InvariantCulture) }
            ];
        }


        #endregion Private Methods
    }
}