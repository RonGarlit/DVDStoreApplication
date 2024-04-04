using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVDStore.DAL.LiveDbTests
{
    [TestClass]
    public class RealLocalMsSqlDbUnitTests
    {
        #region Private Fields

        private DVDStoreDbContext? _context;
        private List<string> tableNames = [];

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        public async Task ActorTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Actors.CountAsync();

            // Assert
            Assert.AreEqual(200, count, "Actor table record count mismatch.");
        }

        [TestMethod]
        public async Task AddressTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Addresses.CountAsync();

            // Assert
            Assert.AreEqual(603, count, "Address table record count mismatch.");
        }

        [TestMethod]
        public async Task CategoryTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Categories.CountAsync();

            // Assert
            Assert.AreEqual(16, count, "Category table record count mismatch.");
        }

        [TestMethod]
        public async Task CityTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Cities.CountAsync();

            // Assert
            Assert.AreEqual(600, count, "City table record count mismatch.");
        }

        [TestMethod]
        public async Task CountryTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Countries.CountAsync();

            // Assert
            Assert.AreEqual(109, count, "Country table record count mismatch.");
        }

        [TestMethod]
        public async Task CustomerTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Customers.CountAsync();

            // Assert
            Assert.AreEqual(599, count, "Customer table record count mismatch.");
        }

        [TestMethod]
        public async Task FilmActorTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Filmactors.CountAsync();

            // Assert
            Assert.AreEqual(5462, count, "FilmActor table record count mismatch.");
        }

        [TestMethod]
        public async Task FilmCategoryTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Filmcategories.CountAsync();

            // Assert
            Assert.AreEqual(1000, count, "FilmCategory table record count mismatch.");
        }

        [TestMethod]
        public async Task FilmRevTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.FilmRevs.CountAsync();

            // Assert
            Assert.AreEqual(1000, count, "FilmRev table record count mismatch.");
        }

        [TestMethod]
        public async Task FilmTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Films.CountAsync();

            // Assert
            Assert.AreEqual(1000, count, "Film table record count mismatch.");
        }

        [TestMethod]
        public async Task FilmTextTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Filmtexts.CountAsync();

            // Assert
            Assert.AreEqual(0, count, "FilmText table should have no records.");
        }

        [TestMethod]
        public async Task InventoryTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Inventories.CountAsync();

            // Assert
            Assert.AreEqual(4581, count, "Inventory table record count mismatch.");
        }

        [TestMethod]
        public async Task LanguageTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Languages.CountAsync();

            // Assert
            Assert.AreEqual(6, count, "Language table record count mismatch.");
        }

        [TestMethod]
        public async Task PaymentTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Payments.CountAsync();
            // Assert
            Assert.AreEqual(16049, count, "Payment table record count mismatch.");
        }

        [TestMethod]
        public async Task RentalTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Rentals.CountAsync();

            // Assert
            Assert.AreEqual(16044, count, "Rental table record count mismatch.");
        }

        [TestMethod]
        public async Task StaffTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Staffs.CountAsync();

            // Assert
            Assert.AreEqual(2, count, "Staff table record count mismatch.");
        }

        [TestMethod]
        public async Task StoreTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.Stores.CountAsync();

            // Assert
            Assert.AreEqual(2, count, "Store table record count mismatch.");
        }

        [TestMethod]
        public async Task TempActorFilmListingTable_HasExpectedRecordCount()
        {
            // Act
            var count = await _context!.TempActorFilmListings.CountAsync();

            // Assert
            Assert.AreEqual(0, count, "TempActorFilmListing table should have no records.");
        }

        [TestMethod]
        public void TestCountTableNames()
        {
            // Arrange
            var expectedCount = 18; // The number of tables in the database 

            // Act
            var count = tableNames.Count;

            // Assert
            Assert.AreEqual(expectedCount, count, "The count of table names is not as expected.");
        }

        [TestInitialize]
        public async Task TestInitializeAsync()
        {
            var options = new DbContextOptionsBuilder<DVDStoreDbContext>()
                .UseSqlServer("Data Source=(local);Initial Catalog=DVDStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .Options;

            _context = new DVDStoreDbContext(options);
            tableNames =
            [
                // Fetch and add the record counts for each table
                $"Payments: {await _context!.Payments.CountAsync()}",
                $"Rentals: {await _context!.Rentals.CountAsync()}",
                $"FilmActors: {await _context!.Filmactors.CountAsync()}",
                $"Inventories: {await _context!.Inventories.CountAsync()}",
                $"FilmRevs: {await _context!.FilmRevs.CountAsync()}",
                $"FilmCategories: {await _context!.Filmcategories.CountAsync()}",
                $"Films: {await _context!.Films.CountAsync()}",
                $"Addresses: {await _context!.Addresses.CountAsync()}",
                $"Cities: {await _context!.Cities.CountAsync()}",
                $"Customers: {await _context!.Customers.CountAsync()}",
                $"Actors: {await _context!.Actors.CountAsync()}",
                $"Countries: {await _context!.Countries.CountAsync()}",
                $"Categories: {await _context!.Categories.CountAsync()}",
                $"Languages: {await _context!.Languages.CountAsync()}",
                $"Stores: {await _context!.Stores.CountAsync()}",
                $"Staff: {await _context!.Staffs.CountAsync()}",
                // Assuming TempActorFilmListings and Filmtexts are also exposed as DbSet properties
                $"TempActorFilmListings: {await _context!.TempActorFilmListings.CountAsync()}",
                $"FilmTexts: {await _context!.Filmtexts.CountAsync()}",
            ]; // Ensure this is reset for each test if needed
        }

        #endregion Public Methods
    }
}
