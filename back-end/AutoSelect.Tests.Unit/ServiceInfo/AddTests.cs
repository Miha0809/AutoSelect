using AutoMapper;
using AutoSelect.API.Models.User;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services;
using AutoSelect.API.Services.Interfaces;
using Moq;

namespace AutoSelect.Tests.Unit.ServiceInfo;

public class AddTests
{
    private readonly Mock<IServiceInfoRepository> _serviceInfoRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IServiceInfoService _serviceInfoService;

    public AddTests()
    {
        _serviceInfoRepositoryMock = new Mock<IServiceInfoRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _serviceInfoService = new ServiceInfoService(_serviceInfoRepositoryMock.Object, _userRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AddAsync_ValidInput_ShouldAddServiceInfo()
    {
        // Arrange
        const string email = "test@example.com";
        var user = new User { Email = email };
        var serviceInfo = new API.Models.Expert.ServiceInfo { Name = "Test Service", Owner = user };
        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email)).ReturnsAsync(user);
        _serviceInfoRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<API.Models.Expert.ServiceInfo>())).Returns(Task.CompletedTask);
        _serviceInfoRepositoryMock.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _serviceInfoService.AddAsync<User>(serviceInfo, email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(serviceInfo.Name, result.Name);
        Assert.Equal(user, result.Owner);
        _serviceInfoRepositoryMock.Verify(repo => repo.AddAsync(It.Is<API.Models.Expert.ServiceInfo>(s => s.Name == serviceInfo.Name && s.Owner == user)), Times.Once);
        _serviceInfoRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task AddAsync_UserNotFound_ShouldThrowException()
    {
        // Arrange
        const string email = "test@example.com";
        var serviceInfo = new API.Models.Expert.ServiceInfo { Name = "Test Service", Owner = null };
        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync<User>(email)).ThrowsAsync(new ArgumentNullException("Owner is null"));

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _serviceInfoService.AddAsync<User>(serviceInfo, email));
    }
}