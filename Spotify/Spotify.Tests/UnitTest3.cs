using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Spotify.Controllers;
using Spotify.Data;
using Spotify.Models;
using Spotify.Services;

namespace Spotify.Tests;

public class UnitTest3
{
    [Fact]
    public async Task Followers_ReturnsViewResultWithFollowers()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        var artist = new Artist
        {
            Followers = new List<User>
            {
                new User { Id = "1", UserName = "User1" },
                new User { Id = "2", UserName = "User2" },
            }
        };
        userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(artist);

        var controller = new ArtistHomeController(userManagerMock.Object, MockDbContext().Object);

        // Act
        var result = await controller.Followers();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<List<User>>(viewResult.Model);
        Assert.Equal(2, model.Count);
    }
    
    [Fact]
    public async Task Followers_ReturnsViewResultWithNoFollowers()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        var artist = new Artist { Followers = new List<User>() };
        userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(artist);

        var controller = new ArtistHomeController(userManagerMock.Object, MockDbContext().Object);

        // Act
        var result = await controller.Followers();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<List<User>>(viewResult.Model);
        Assert.Empty(model);
    }
    private Mock<ApplicationDbContext> MockDbContext()
    {
        var dbContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

        return dbContext;
    }
}