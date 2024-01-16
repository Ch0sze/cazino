using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace YourNamespace.Tests.Admin
{
    public class AdminControllerTests
    {
        [Fact]
        public async Task SetAdmin_Success()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<cazinoUser>>();
            var adminController = new AdminController(userManagerMock.Object);

            var userEmail = "test@example.com";
            var user = new cazinoUser { Email = userEmail };

            userManagerMock.Setup(um => um.FindByEmailAsync(userEmail))
                .ReturnsAsync(user);

            // Act
            var actionResult = await adminController.SetAdmin(userEmail);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(actionResult);
            dynamic jsonData = jsonResult.Value;

            Assert.True(jsonData.success);
            Assert.Equal("User was added admin privileges.", jsonData.message);

            userManagerMock.Verify(um => um.UpdateAsync(user), Times.Once);
            Assert.True(user.IsAdmin);

            // Additional assertion to check the database state
            Assert.True(DatabaseFake.Users[0].IsAdmin); // Assuming DatabaseFake.Users is a collection holding users
        }

        [Fact]
        public async Task SetAdmin_UserNotFound()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<cazinoUser>>();
            var adminController = new AdminController(userManagerMock.Object);

            var userEmail = "nonexistent@example.com";

            userManagerMock.Setup(um => um.FindByEmailAsync(userEmail))
                .ReturnsAsync((cazinoUser)null);

            // Act
            var actionResult = await adminController.SetAdmin(userEmail);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(actionResult);
            dynamic jsonData = jsonResult.Value;

            Assert.False(jsonData.success);
            Assert.Equal("User not found.", jsonData.message);

            userManagerMock.Verify(um => um.UpdateAsync(It.IsAny<cazinoUser>()), Times.Never);

            // Additional assertion to check the database state (assuming DatabaseFake.Users is a collection holding users)
            Assert.Empty(DatabaseFake.Users);
        }
    }
}
