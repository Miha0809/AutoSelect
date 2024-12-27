using AutoMapper;
using AutoSelect.API.DTOs.User.Requests;
using AutoSelect.API.Models.Client;
using AutoSelect.API.Models.Enums;
using AutoSelect.API.Models.Expert;
using AutoSelect.API.Models.User;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AutoSelect.Tests.Unit.UserService;

public class UpdateTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IUserService _userService;

    public UpdateTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        _mapperMock = new Mock<IMapper>();
        _userService = new API.Services.UserService(_userRepositoryMock.Object, _userManagerMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task UpdateAsync_ValidInput_ShouldUpdateFirstNameAndLastName()
    {
        // Arrange
        const string email = "test@gmail.com";
        const string firstName = "Mark";
        const string lastName = "Full";
        var user = new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };

        const string updateFirstName = "Johny";
        const string updateLastName = "Smith";
        var updateProfileDto = new UpdateProfileAfterFirstLoginDto
        {
            FirstName = updateFirstName,
            LastName = updateLastName,
            IsExpert = false
        };

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email))
                           .ReturnsAsync(user);

        _mapperMock.Setup(mapper => mapper.Map(updateProfileDto, user))
                   .Callback((UpdateProfileAfterFirstLoginDto dto, User u) =>
                   {
                       u.FirstName = dto.FirstName;
                       u.LastName = dto.LastName;
                   });

        _userManagerMock.Setup(um => um.GetRolesAsync(user))
                        .ReturnsAsync(new List<string>());

        // Act
        await _userService.UpdateAsync<User, UpdateProfileAfterFirstLoginDto>(updateProfileDto, email);

        // Assert
        Assert.Equal(updateFirstName, user.FirstName);
        Assert.Equal(updateLastName, user.LastName);
    }

    [Fact]
    public async Task UpdateAsync_UserHasRole_ShouldUpdateUser()
    {
        // Arrange
        var email = "test@example.com";
        var updateDto = new UpdateProfileDto { FirstName = "John", LastName = "Doe" };
        var user = new User { Email = email };

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email)).ReturnsAsync(user);
        _userManagerMock.Setup(manager => manager.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Client" });

        // Act
        await _userService.UpdateAsync<User, UpdateProfileDto>(updateDto, email);

        // Assert
        _mapperMock.Verify(mapper => mapper.Map(updateDto, user), Times.Once);
        _userManagerMock.Verify(manager => manager.UpdateAsync(user), Times.Once);
        _userRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_UserIsExpertAndHasNoRole_ShouldAddExpertRole()
    {
        // Arrange
        var email = "expert@example.com";
        var updateDto = new UpdateProfileAfterFirstLoginDto { FirstName = "Name", LastName = "Full", IsExpert = true };
        var user = new User { Email = email };

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email)).ReturnsAsync(user);
        _userManagerMock.Setup(manager => manager.GetRolesAsync(user)).ReturnsAsync(new List<string>());

        // Act
        await _userService.UpdateAsync<User, UpdateProfileAfterFirstLoginDto>(updateDto, email);

        // Assert
        _userRepositoryMock.Verify(repo => repo.Add(It.IsAny<Expert>()), Times.Once);
        _userManagerMock.Verify(manager => manager.AddToRoleAsync(It.IsAny<Expert>(), nameof(Roles.Expert)), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_UserIsNotExpertAndHasNoRole_ShouldAddClientRole()
    {
        // Arrange
        var email = "client@example.com";
        var updateDto = new UpdateProfileAfterFirstLoginDto { FirstName = "Mark", LastName = "Full", IsExpert = false };
        var user = new User { Email = email };

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email)).ReturnsAsync(user);
        _userManagerMock.Setup(manager => manager.GetRolesAsync(user)).ReturnsAsync(new List<string>());

        // Act
        await _userService.UpdateAsync<User, UpdateProfileAfterFirstLoginDto>(updateDto, email);

        // Assert
        _userRepositoryMock.Verify(repo => repo.Add(It.IsAny<Client>()), Times.Once);
        _userManagerMock.Verify(manager => manager.AddToRoleAsync(It.IsAny<Client>(), nameof(Roles.Client)), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_UserNotFound_ShouldThrowException()
    {
        // Arrange
        var email = "notfound@example.com";
        var updateDto = new UpdateProfileDto()
        {
            FirstName = "Mark",
            LastName = "Full"
        };

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email)).ReturnsAsync((User?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.UpdateAsync<User, UpdateProfileDto>(updateDto, email));
    }
}