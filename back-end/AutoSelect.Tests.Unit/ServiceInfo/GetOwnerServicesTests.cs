using AutoMapper;
using AutoSelect.API.Models.User;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services;
using AutoSelect.API.Services.Interfaces;
using Moq;

namespace AutoSelect.Tests.Unit.ServiceInfo;

public class GetOwnerServicesTests
{
    private readonly Mock<IServiceInfoRepository> _serviceInfoRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IServiceInfoService _serviceInfoService;

    public GetOwnerServicesTests()
    {
        _serviceInfoRepositoryMock = new Mock<IServiceInfoRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _serviceInfoService = new ServiceInfoService(_serviceInfoRepositoryMock.Object, _userRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetOwnerServicesAsync_ValidInput_ShouldReturnOwnerServices()
    {
        // Arrage
        const string email = "test@gmail.com";
        var owner = new User() { Email = email };
        var services = new List<API.Models.Expert.ServiceInfo>() {
            new() {Name = "testName", Owner = owner},
            new() {Name = "testName2", Owner = owner}
        };

        _serviceInfoRepositoryMock.Setup(repo => repo.GetOwnerServicesAsync(email)).ReturnsAsync(services);

        // Act
        var result = await _serviceInfoService.GetOwnerServicesAsync(email);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(services.Count, result.ToArray().Length);

        for (int i = 0; i < result.Count(); i++)
        {
            Assert.Equal(services[i].Name, result.ToArray()[i].Name);
            Assert.Equal(services[i].Owner, result.ToArray()[i].Owner);
        }

        _serviceInfoRepositoryMock.Verify(repo => repo.GetOwnerServicesAsync(email), Times.AtMostOnce);
    }

    [Fact]
    public async Task GetOwnerServicesAsync_OwnerNotExistWithEmail_ShouldReturnEmptyList()
    {
        // Arrage
        const string email = "test@gmail.com";
        _serviceInfoRepositoryMock.Setup(repo => repo.GetOwnerServicesAsync(email)).ReturnsAsync(new List<API.Models.Expert.ServiceInfo>());

        // Act
        var result = await _serviceInfoService.GetOwnerServicesAsync(email);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _serviceInfoRepositoryMock.Verify(repo => repo.GetOwnerServicesAsync(email), Times.AtMostOnce);
    }

    [Fact]
    public async Task GetOwnerServicesAsync_EmailIsNull_ShouldReturnEmptyList()
    {
        // Arrage
        const string email = null;
        _serviceInfoRepositoryMock.Setup(repo => repo.GetOwnerServicesAsync(email)).ReturnsAsync(new List<API.Models.Expert.ServiceInfo>());

        // Act
        var result = await _serviceInfoService.GetOwnerServicesAsync(email);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _serviceInfoRepositoryMock.Verify(repo => repo.GetOwnerServicesAsync(email), Times.AtMostOnce);
    }
}