using AutoMapper;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AutoSelect.Tests.Unit.UserService;

public class GetAllUsersTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<UserManager<API.Models.User.User>> _userManagerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IUserService _userService;

    public GetAllUsersTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userManagerMock = new Mock<UserManager<API.Models.User.User>>(Mock.Of<IUserStore<API.Models.User.User>>(), null, null, null, null, null, null, null, null);
        _mapperMock = new Mock<IMapper>();
        _userService = new API.Services.UserService(_userRepositoryMock.Object, _userManagerMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllUsersAsync_ValidInput_ShouldReturnAllUsers()
    {
        // Arrange
        var users = new List<API.Models.User.User>()
        {
            new()
            {
                Email = "test@gmail.com",
                FirstName = "Mark",
                LastName = "Full",
            },
            new()
            {
                Email = "a@gmail.com",
                FirstName = "Johny",
                LastName = "Seens"
            }
        };
        _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync<API.Models.User.User>()).ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllUsersAsync<API.Models.User.User>();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(users.Count, result.ToArray().Length);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetAllUsersAsync_NoUsersExist_ShouldReturnEmptyList()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync<API.Models.User.User>()).ReturnsAsync(new List<API.Models.User.User>());

        // Act
        var result = await _userService.GetAllUsersAsync<API.Models.User.User>();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllUsersAsync_ValidInput_ShouldReturnCorrectTypes()
    {
        // Arrange
        var users = new List<API.Models.User.User> { new(), new() };
        _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync<API.Models.User.User>()).ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllUsersAsync<API.Models.User.User>();

        // Assert
        Assert.All(result, user => Assert.IsType<API.Models.User.User>(user));
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(users.Count, result.ToArray().Length);
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldCallRepositoryOnce()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync<API.Models.User.User>()).ReturnsAsync(new List<API.Models.User.User>());

        // Act
        await _userService.GetAllUsersAsync<API.Models.User.User>();

        // Assert
        _userRepositoryMock.Verify(repo => repo.GetAllUsersAsync<API.Models.User.User>(), Times.Once);
    }

    [Fact]
    public async Task GetAllUsersAsync_RepositoryThrows_ShouldThrowException()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync<API.Models.User.User>()).ThrowsAsync(new Exception());

        // Act && Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.GetAllUsersAsync<API.Models.User.User>());
    }
}
