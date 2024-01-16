using System.Threading.Tasks;
using cazino3.Areas.Identity.Data;
using cazino3.Controllers;
using Microsoft.AspNetCore.Identity;
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
            var userManagerMock = new Mock<Microsoft.AspNetCore.Identity.UserManager<cazinoUser>>();
            var adminController = new AdminController(userManagerMock.Object);

            var userEmail = "test@example.com";
            var user = new cazinoUser { Email = userEmail };

            userManagerMock.Setup(um => um.FindByEmailAsync(userEmail))
                .ReturnsAsync(user);

            // Act
            var actionResult = await adminController.SetAdmin(userEmail);

            // Assert
            var jsonResult = Xunit.Assert.IsType<JsonResult>(actionResult);
            dynamic jsonData = jsonResult.Value;

            Xunit.Assert.True(jsonData.success);
            Xunit.Assert.Equal("User was added admin privileges.", jsonData.message);

            userManagerMock.Verify(um => um.UpdateAsync(user), Times.Once);
            Xunit.Assert.True(user.IsAdmin);
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
            var jsonResult = Xunit.Assert.IsType<JsonResult>(actionResult);
            dynamic jsonData = jsonResult.Value;

            Xunit.Assert.False(jsonData.success);
            Xunit.Assert.Equal("User not found.", jsonData.message);

            userManagerMock.Verify(um => um.UpdateAsync(It.IsAny<cazinoUser>()), Times.Never);
        }
    }
}