using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Spotify.Controllers;
using Spotify.Models;

namespace Spotify.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            
            var claims = new[] { new Claim(ClaimTypes.Name, "testuser") };
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthType"));
            
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(c => c.User).Returns(userPrincipal);
            
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext.Object
            };

            var controller = new HomeController(userManagerMock.Object)
            {
                ControllerContext = controllerContext
            };

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_ReturnsViewResult()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var controller = new HomeController(userManagerMock.Object);

            // Act
            var result = controller.Privacy();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}