using AutoMapper;
using AutoSelect.API.Models.User;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services;
using AutoSelect.API.Services.Interfaces;
using Moq;

namespace AutoSelect.Tests.Unit.ServiceInfo;

public class DeleteTests
{
    private readonly Mock<IServiceInfoRepository> _serviceInfoRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IServiceInfoService _serviceInfoService;

    public DeleteTests()
    {
        _serviceInfoRepositoryMock = new Mock<IServiceInfoRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _serviceInfoService = new ServiceInfoService(_serviceInfoRepositoryMock.Object, _userRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task DeleteAsync_ValidInput_ShouldDeleteServiceInfo()
    {
        // Arrange
        const string email = "test@gmail.com";
        const int id = 1;
        var owner = new User { Email = email };
        var serviceInfo = new API.Models.Expert.ServiceInfo { Id = id, Price = 100, Name = "Old Service", Owner = owner };

        _serviceInfoRepositoryMock.Setup(repo => repo.GetServiceInfoByIdAsync(id)).ReturnsAsync(serviceInfo);
        _serviceInfoRepositoryMock.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _serviceInfoService.DeleteAsync(id);

        // Assert
        Assert.True(result);
        _serviceInfoRepositoryMock.Verify(repo => repo.GetServiceInfoByIdAsync(id), Times.AtMostOnce);
        _serviceInfoRepositoryMock.Verify(repo => repo.Delete(serviceInfo), Times.AtMostOnce);
        _userRepositoryMock.Verify(repo => repo.SaveAsync(), Times.AtMostOnce);
    }

    [Fact]
    public async Task DeleteAsync_ServiceInfoNotFound_ShouldReturnFalse()
    {
        // Arrange
        const int id = 1;

        _serviceInfoRepositoryMock.Setup(repo => repo.GetServiceInfoByIdAsync(id)).ReturnsAsync((API.Models.Expert.ServiceInfo)null);

        // Act
        var result = await _serviceInfoService.DeleteAsync(id);

        // Assert
        Assert.False(result);
        
        _serviceInfoRepositoryMock.Verify(repo => repo.GetServiceInfoByIdAsync(id), Times.Once);
        _serviceInfoRepositoryMock.Verify(repo => repo.Delete(It.IsAny<API.Models.Expert.ServiceInfo>()), Times.Never);
        _serviceInfoRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Never);
    }
}
