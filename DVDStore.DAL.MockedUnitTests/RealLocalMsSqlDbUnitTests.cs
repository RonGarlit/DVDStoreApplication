using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVDStore.DAL.MockedUnitTests
{
    [TestClass]
    public class RealLocalMsSqlDbUnitTests
    {
        private DVDStoreDbContext? _context;
        private List<string> tableNames = new List<string>();

        [TestInitialize]
        public async Task TestInitializeAsync()
        {
            var options = new DbContextOptionsBuilder<DVDStoreDbContext>()
                .UseSqlServer("Data Source=(local);Initial Catalog=DVDStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .Options;

            _context = new DVDStoreDbContext(options);

            // Assuming you have a method to retrieve table names or other relevant data for your tests
            // This is an example of how you might approach it within the constraints of EF Core
            // For demo purposes, let's assume we're counting entities in a known table
            var actorCount = await _context.Actors.CountAsync();
            tableNames.Add($"Actors: {actorCount}");

            // Repeat the above for other entities as needed
        }

        [TestMethod]
        public void TestCountTableNames()
        {
            // Arrange
            var expectedCount = 1; // Adjust based on your setup and what you add in TestInitialize

            // Act
            var count = tableNames.Count;

            // Assert
            Assert.AreEqual(expectedCount, count, "The count of table names is not as expected.");
        }
    }
}
