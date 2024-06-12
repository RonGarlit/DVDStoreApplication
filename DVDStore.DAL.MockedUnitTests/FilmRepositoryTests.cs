using DVDStore.Web.MVC.Areas.FilmCatalog.Common;
using DVDStore.Web.MVC.Areas.FilmCatalog.Models;
using DVDStore.Web.MVC.Areas.FilmCatalog.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DVDStore.DAL.MockedUnitTests
{
    [TestClass]
    public class FilmRepositoryTests
    {
        #region Private Fields

        private FilmRepository? _filmRepository;
        private DbContextOptions<DVDStoreDbContext>? _options;
        private FilmsPropertyMapper? _propertyMapper;
        private DVDStoreDbContext? context;

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        public async Task AddFilm_AddsFilmSuccessfully()
        {
            var filmViewModel = new FilmViewModel
            {
                Filmid = 11,
                Title = "NEW FILM",
                Description = "Description",
                Releaseyear = "2022",
                Languageid = 1,
                Rentalduration = 5,
                Rentalrate = 4.99m,
                Length = 120,
                Replacementcost = 25.99m,
                Rating = "PG",
                Specialfeatures = "Behind the Scenes",
                Lastupdate = DateTime.Now
            };

            var result = await _filmRepository!.AddFilm(filmViewModel);

            Assert.IsNotNull(result);
            Assert.AreEqual("NEW FILM", result.Title);

            var filmInDb = await context!.Films.FindAsync(11);
            Assert.IsNotNull(filmInDb);
            Assert.AreEqual("NEW FILM", filmInDb.Title);
        }

        [TestMethod]
        public async Task GetFilm_ReturnsCorrectFilm()
        {
            var result = await _filmRepository!.GetFilm(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("ACADEMY DINOSAUR", result.Title);
        }

        [TestMethod]
        public async Task GetPagedFilms_ReturnsCorrectPagedResults()
        {
            var resourceParameters = new FilmCatalogResourceParameters
            {
                PageNumber = 1,
                PageSize = 5
            };

            var result = await _filmRepository!.GetPagedFilms(resourceParameters);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(10, result.TotalCount);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            context = new DVDStoreDbContext(_options);
            context.Database.EnsureDeleted();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _options = new DbContextOptionsBuilder<DVDStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_DVDStore")
                .Options;

            context = new DVDStoreDbContext(_options);
            _propertyMapper = new FilmsPropertyMapper();
            _filmRepository = new FilmRepository(context, _propertyMapper);

            context.Actors.AddRange(GetActorList());
            context.Films.AddRange(GetFilmList());
            context.Customers.AddRange(GetCustomerList());
            context.Categories.AddRange(GetCategoriesList());
            context.Filmcategories.AddRange(GetFilmCategoriesList());
            context.SaveChanges();
        }

        [TestMethod]
        public async Task UpdateFilm_UpdatesFilmSuccessfully()
        {
            var filmViewModel = new FilmViewModel
            {
                Filmid = 1,
                Title = "UPDATED TITLE",
                Description = "Updated Description",
                Releaseyear = "2022",
                Languageid = 1,
                Rentalduration = 5,
                Rentalrate = 4.99m,
                Length = 120,
                Replacementcost = 25.99m,
                Rating = "PG",
                Specialfeatures = "Behind the Scenes",
                Lastupdate = DateTime.Now
            };

            var result = await _filmRepository!.UpdateFilm(filmViewModel);

            Assert.IsNotNull(result);
            Assert.AreEqual("UPDATED TITLE", result.Title);

            var filmInDb = await context!.Films.FindAsync(1);
            Assert.IsNotNull(filmInDb);
            Assert.AreEqual("UPDATED TITLE", filmInDb.Title);
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

        private static List<Category> GetCategoriesList()
        {
            return
            [
                new Category { Categoryid = 1, Name = "Action", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 2, Name = "Animation", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 3, Name = "Children", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 4, Name = "Classics", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 5, Name = "Comedy", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 6, Name = "Documentary", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 7, Name = "Drama", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 8, Name = "Family", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 9, Name = "Foreign", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 10, Name = "Games", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 11, Name = "Horror", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 12, Name = "Music", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 13, Name = "New", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 14, Name = "Sci-Fi", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 15, Name = "Sports", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) },
                new Category { Categoryid = 16, Name = "Travel", Lastupdate = DateTime.Parse("2006-02-15 04:46:27", CultureInfo.InvariantCulture) }
            ];
        }

        private static List<Customer> GetCustomerList()
        {
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

        private static List<Filmcategory> GetFilmCategoriesList()
        {
            return
            [
                new Filmcategory { Filmid = 1, Categoryid = 6, Lastupdate = DateTime.Parse("2006-02-15 05:07:09", CultureInfo.InvariantCulture) },
                new Filmcategory { Filmid = 2, Categoryid = 11, Lastupdate = DateTime.Parse("2006-02-15 05:07:09", CultureInfo.InvariantCulture) },
                new Filmcategory { Filmid = 3, Categoryid = 6, Lastupdate = DateTime.Parse("2006-02-15 05:07:09", CultureInfo.InvariantCulture) },
                new Filmcategory { Filmid = 4, Categoryid = 11, Lastupdate = DateTime.Parse("2006-02-15 05:07:09", CultureInfo.InvariantCulture) },
                new Filmcategory { Filmid = 5, Categoryid = 8, Lastupdate = DateTime.Parse("2006-02-15 05:07:09", CultureInfo.InvariantCulture) },
                new Filmcategory { Filmid = 6, Categoryid = 9, Lastupdate = DateTime.Parse("2006-02-15 05:07:09", CultureInfo.InvariantCulture) },
                new Filmcategory { Filmid = 7, Categoryid = 5, Lastupdate = DateTime.Parse("2006-02-15 05:07:09", CultureInfo.InvariantCulture) },
                new Filmcategory { Filmid = 8, Categoryid = 11, Lastupdate = DateTime.Parse("2006-02-15 05:07:09", CultureInfo.InvariantCulture) },
                new Filmcategory { Filmid = 9, Categoryid = 11, Lastupdate = DateTime.Parse("2006-02-15 05:07:09", CultureInfo.InvariantCulture) },
                new Filmcategory { Filmid = 10, Categoryid = 15, Lastupdate = DateTime.Parse("2006-02-15 05:07:09", CultureInfo.InvariantCulture) }
            ];
        }

        private static List<Film> GetFilmList()
        {
            return
            [
                new Film { Filmid = 1, Title = "ACADEMY DINOSAUR", Description = "A Epic Drama of a Feminist And a Mad Scientist who must Battle a Teacher in The Canadian Rockies", Releaseyear = "2006", Languageid = 1, Rentalduration = 6, Rentalrate = 0.99m, Length = 86, Replacementcost = 20.99m, Rating = "PG", Specialfeatures = "Deleted Scenes,Behind the Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000",CultureInfo.InvariantCulture) },
                new Film { Filmid = 2, Title = "ACE GOLDFINGER", Description = "A Astounding Epistle of a Database Administrator And a Explorer who must Find a Car in Ancient China", Releaseyear = "2006", Languageid = 1, Rentalduration = 3, Rentalrate = 4.99m, Length = 48, Replacementcost = 12.99m, Rating = "G", Specialfeatures = "Trailers,Deleted Scenes", Lastupdate = DateTime.Parse("2006-02-15 05:03:42.000",CultureInfo.InvariantCulture) },
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