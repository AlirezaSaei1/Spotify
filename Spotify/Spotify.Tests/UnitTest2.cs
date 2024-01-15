using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Spotify.Controllers;
using Spotify.Models;

namespace Spotify.Tests;

public class UnitTest2
{
    [Fact]
    public void Index_ReturnsViewResult()
    {
        // Arrange
        var controller = new UserHomeController(MockUserManager().Object);

        // Act
        var result = controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    private Mock<UserManager<ApplicationUser>> MockUserManager()
    {
        var users = new List<ApplicationUser>
        {
            new User { Id = "1", UserName = "user1" },
            new Artist { Id = "2", UserName = "artist1" }
        }.AsQueryable();

        var store = new Mock<IUserStore<ApplicationUser>>();
        var userManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

        userManager.Setup(x => x.Users).Returns(users);
        userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((Func<ApplicationUser?>)null);

        return userManager;
    }
}

