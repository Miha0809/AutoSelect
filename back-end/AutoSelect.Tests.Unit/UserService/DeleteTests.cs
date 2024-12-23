using AutoMapper;
using AutoSelect.API.Models.User;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AutoSelect.Tests.Unit.UserService;

public class DeleteTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IUserService _userService;

    public DeleteTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        _mapperMock = new Mock<IMapper>();
        _userService = new API.Services.UserService(_userRepositoryMock.Object, _userManagerMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task DeleteAsync_Should_ReturnDeletedUser()
    {
        // Arrange
        const string email = "test@gmail.com";
        var user = new User() { Email = email };

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email))
                            .ReturnsAsync(user);
        _userRepositoryMock.SetupSequence(repo => repo.GetUserByEmailAsync<User>(email))
                            .ReturnsAsync(user)
                            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.DeleteAsync<User>(email);

        // Assert
        Assert.True(result);
        _userRepositoryMock.Verify(repo => repo.Remove(user), Times.Once);
        _userRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCheckCallRepository()
    {
        // Arrage
        const string email = "test@gmail.com";
        var user = new User() {Email=email};
        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email)).ReturnsAsync(user);

        // Act
        _ = await _userService.DeleteAsync<User>(email);

        // Assert
        _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync<User>(email), Times.AtLeastOnce);
        _userRepositoryMock.Verify(repo => repo.Remove(user), Times.Once);
        _userRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowArgumentNullException_WhenUserNotExist()
    {
        // Arrage
        const string email = "test@gmail.com";
        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email)).ReturnsAsync((User?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.DeleteAsync<User>(email));
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenUserIsNotDeleted()
    {
        // Arrage
        const string email = "test@gmail.com";
        var user = new User { Email = email };

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email))
                           .ReturnsAsync(user);

        _userRepositoryMock.SetupSequence(repo => repo.GetUserByEmailAsync<User>(email))
                           .ReturnsAsync(user)
                           .ReturnsAsync(user);

        // Act
        var result = await _userService.DeleteAsync<User>(email);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        const string email = "test@gmail.com";
        var user = new User { Email = email };

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email))
                           .ReturnsAsync(user);
        _userRepositoryMock.Setup(repo => repo.Remove(user))
                           .Throws(new Exception("Delete error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.DeleteAsync<User>(email));
    }
}
