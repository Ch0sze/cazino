using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using UTB.Eshop.Application.Abstractions;
using UTB.Eshop.Web.Controllers;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using UTB.Eshop.Application.Implementation;

namespace UTB.Eshop.Tests.Controllers
{
    [TestClass]
    public class BalanceControllerTests
    {

        [TestMethod]
        public async Task GetBalance_HandlesException_ReturnsStatusCode500()
        {
            // Arrange
            var balanceServiceMock = new Mock<IBalanceService>();
            balanceServiceMock.Setup(x => x.GetBalanceAsync(It.IsAny<string>())).ThrowsAsync(new Exception("Test Exception"));

            var controller = new BalanceController(balanceServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "testUser"),
                    }, "test"))
                }
            };

            // Act
            var result = await controller.GetBalance() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual("An error occurred while retrieving the balance.", result.Value);
        }
    }
}
