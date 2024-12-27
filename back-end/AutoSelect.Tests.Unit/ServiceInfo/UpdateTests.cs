using AutoMapper;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services;
using AutoSelect.API.Services.Interfaces;
using Moq;
using AutoSelect.API.DTOs.Expert.Responses;
using AutoSelect.API.Models.User;

namespace AutoSelect.Tests.Unit.ServiceInfo;

public class UpdateTests
{
    private readonly Mock<IServiceInfoRepository> _serviceInfoRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IServiceInfoService _serviceInfoService;

    public UpdateTests()
    {
        _serviceInfoRepositoryMock = new Mock<IServiceInfoRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _serviceInfoService = new ServiceInfoService(_serviceInfoRepositoryMock.Object, _userRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task UpdateAsync_ValidInput_ShouldUpdateServiceInfo()
    {
        // Arrange
        const string email = "test@gmail.com";
        var owner = new User { Email = email };
        var serviceInfo = new API.Models.Expert.ServiceInfo { Id = 1, Price = 100, Name = "Old Service", Owner = owner };
        var serviceInfoUpdateDto = new ServiceInfoDto { Id = 1, Price = 123, Name = "Updated Service" };

        _serviceInfoRepositoryMock.Setup(repo => repo.GetServiceInfoByIdAsync(serviceInfoUpdateDto.Id)).ReturnsAsync(serviceInfo);
        _mapperMock.Setup(mapper => mapper.Map(serviceInfoUpdateDto, serviceInfo))
                   .Callback((ServiceInfoDto dto, API.Models.Expert.ServiceInfo u) =>
                   {
                       serviceInfo.Name = dto.Name;
                       serviceInfo.Price = dto.Price;
                   });;
        _serviceInfoRepositoryMock.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _serviceInfoService.UpdateAsync(serviceInfoUpdateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(serviceInfoUpdateDto.Name, result.Name);
        Assert.Equal(email, result.Owner.Email);
        
        _mapperMock.Verify(mapper => mapper.Map(serviceInfoUpdateDto, serviceInfo), Times.Once);
        _serviceInfoRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ServiceInfoNotFound_ShouldThrowException()
    {
        // Arrange
        var serviceInfoUpdateDto = new ServiceInfoDto { Id = 1, Price = 123, Name = "Updated Service" };
        _serviceInfoRepositoryMock.Setup(repo => repo.GetServiceInfoByIdAsync(serviceInfoUpdateDto.Id)).ReturnsAsync((API.Models.Expert.ServiceInfo)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _serviceInfoService.UpdateAsync(serviceInfoUpdateDto));
    }
}
