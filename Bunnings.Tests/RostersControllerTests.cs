using Bunnings.API.Controllers;
using Bunnings.Domain.Entities;
using Bunnings.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bunnings.Tests
{
    public class RostersControllerTests
    {
        private readonly Mock<IRosterService> rosterServiceMock;
        private readonly RostersController rosersController;

        public RostersControllerTests()
        {
            rosterServiceMock = new Mock<IRosterService>();
            rosersController = new RostersController(rosterServiceMock.Object);
        }

        [Fact]
        public async Task GetRosters_DateQueryParamsNotProvided_ShouldReturnNoContent()
        {
            // Arrange
            DateTime? fromDate = null;
            DateTime? toDate = null;

            // Act
            dynamic result = await rosersController.GetRosters(fromDate, toDate);

            // Assert
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task GetRosters_DateQueryParamsProvided_ShouldReturnContent()
        {
            // Arrange
            rosterServiceMock.Setup(rs => rs.GetRosters(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult(new Roster[] { new Roster() }));

            // Act
            dynamic result = await rosersController.GetRosters(DateTime.Now, DateTime.Now.AddDays(1));

            // Assert
            Assert.Equal(200, result.StatusCode);
            Assert.NotEmpty(result.Value);
        }

        [Fact]
        public async Task SaveRoster_InvalidRosterModel_ShouldNotSave()
        {
            // Arrange
            rosterServiceMock.Setup(rs => rs.SaveRoster(It.IsAny<Roster>())).Returns(Task.FromResult(true));
            rosersController.ModelState.AddModelError("Model", "Error");

            // Act
            dynamic result = await rosersController.SaveRoster(new Roster());

            // Assert
            rosterServiceMock.Verify(rs => rs.SaveRoster(It.IsAny<Roster>()), Times.Never);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
