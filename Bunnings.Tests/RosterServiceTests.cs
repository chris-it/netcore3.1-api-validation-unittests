using Bunnings.Domain.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Bunnings.Tests
{
    public class RosterServiceTests
    {
        private readonly RosterService rosterService;
        public RosterServiceTests()
        {
            rosterService = new RosterService();
        }

        [Theory]
        [InlineData("2020-12-01", "2020-12-01")] // Same from and to dates
        [InlineData("2020-12-02", "2020-12-01")] // From date is greater than to date
        public async Task GetRosters_InvalidDates_ShouldReturnEmptyList(string fromDate, string toDate)
        {
            // Arrange
            //(Inline data above)

            // Act
            var result = await rosterService.GetRosters(DateTime.Parse(fromDate), DateTime.Parse(toDate));

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetRosters_ValidDates_ShouldReturnData()
        {
            // Arrange
            DateTime fromDate = DateTime.Now.AddDays(-100);
            DateTime toDate = DateTime.Now.AddDays(100);

            // Act
            var result = await rosterService.GetRosters(fromDate, toDate);

            // Assert
            Assert.NotEmpty(result);
        }
    }
}
