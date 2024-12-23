using AutoMapper;
using AutoSelect.API.Models.Expert;
using AutoSelect.API.Models.User;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AutoSelect.Tests.Unit.UserService;

public class GetUserTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IUserService _userService;

    public GetUserTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        _mapperMock = new Mock<IMapper>();
        _userService = new API.Services.UserService(_userRepositoryMock.Object, _userManagerMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetUserAsync_Should_ReturnSingleUser()
    {
        // Arrange
        const string email = "test@gmail.com";
        const string firstName = "Mark";
        const string lastName = "Full";
        var user = new Expert() { Email = email, FirstName = firstName, LastName = lastName };
        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<Expert>(email)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserAsync<Expert>(email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(email, result.Email);
        Assert.Equal(firstName, result.FirstName);
        Assert.Equal(lastName, result.LastName);
    }

    [Fact]
    public async Task GetUserAsync_Should_ReturnNullWhenUserNotExist()
    {
        // Arrange
        const string email = "test@gmail.com";
        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email)).ReturnsAsync((User?)null);

        // Act
        var result = await _userService.GetUserAsync<User>(email);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetUserAsync_Should_ReturnThrowException_WhenRepositoryThrow()
    {
        // Arrange
        const string email = "test@gmail.com";
        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email)).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.GetUserAsync<User>(email));
    }

    [Fact]
    public async Task GetUserAsync_ShouldCallRepositoryOnce()
    {
        // Arrange
        const string email = "test@gmail.com";
        var user = new User() { Email = email };
        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email)).ReturnsAsync(user);

        // Act
        _ = await _userService.GetUserAsync<User>(email);

        // Assert
        _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync<User>(email), Times.Once);
    }

    [Fact]
    public async Task GetUserAsync_Should_ReturnCorrectType()
    {
        // Arrange
        const string email = "test@gmail.com";
        var expert = new Expert() {Email = email};
        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<Expert>(email)).ReturnsAsync(expert);

        // Act
        var result = await _userService.GetUserAsync<Expert>(email);

        // Assert
        Assert.IsType<Expert>(result);
        Assert.Equal(email, result.Email);
    }
}