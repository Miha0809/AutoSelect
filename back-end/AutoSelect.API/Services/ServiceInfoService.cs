using AutoMapper;
using AutoSelect.API.Models.Expert;
using AutoSelect.API.DTOs.Expert.Responses;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services.Interfaces;

namespace AutoSelect.API.Services;

/// <summary>
/// Сервіс послуг експерта.
/// </summary>
/// <param name="serviceInfoRepository">Репозіторі послуг експерта.</param>
/// <param name="userRepository">Репозіторі користувача.</param>
/// <param name="mapper">Маппер об'єктів.</param>
public class ServiceInfoService(IServiceInfoRepository serviceInfoRepository, IUserRepository userRepository, IMapper mapper) : IServiceInfoService
{
    /// <summary>
    /// Всі послуги конкретного експерта.
    /// </summary>
    /// <param name="email">Електрона пошта власника послуги.</param>
    async Task<IEnumerable<ServiceInfo>?> IServiceInfoService.GetOwnerServicesAsync(string email)
    {
        var services = await serviceInfoRepository.GetOwnerServicesAsync(email);

        return services;
    }

    /// <summary>
    /// Добавити нову послугу.
    /// </summary>
    /// <param name="serviceInfo">Послуга.</param>
    /// <param name="email">Електрона пошта власника послуги.</param>
    async Task<ServiceInfo> IServiceInfoService.AddAsync<TUser>(ServiceInfo serviceInfo, string email)
    {
        var owner = await userRepository.GetUserByEmailAsync<TUser>(email);

        if (owner is null)
        {
            throw new ArgumentNullException("Owner is null");
        }

        var service = new ServiceInfo(serviceInfo)
        {
            Name = serviceInfo.Name,
            Owner = owner!
        };

        await serviceInfoRepository.AddAsync(service);
        await serviceInfoRepository.SaveAsync();

        return service;
    }

    /// <summary>
    /// Редагування послуги.
    /// </summary>
    /// <param name="serviceInfoUpdateDto">Послуга з новими даними.</param>
    async Task<ServiceInfo> IServiceInfoService.UpdateAsync(ServiceInfoDto serviceInfoUpdateDto)
    {
        var service = await serviceInfoRepository.GetServiceInfoByIdAsync(serviceInfoUpdateDto.Id);

        if (service is null)
        {
            throw new ArgumentException("service is null");
        }

        mapper.Map(serviceInfoUpdateDto, service);
        await serviceInfoRepository.SaveAsync();

        return (await serviceInfoRepository.GetServiceInfoByIdAsync(serviceInfoUpdateDto.Id))!;
    }

    /// <summary>
    /// Видалення конкретної послуги.
    /// </summary>
    /// <param name="id">Ідентифікатор послуги.</param>
    async Task<bool> IServiceInfoService.DeleteAsync(int id)
    {
        var service = await serviceInfoRepository.GetServiceInfoByIdAsync(id);

        if (service is null)
        {
            return false;
        }

        serviceInfoRepository.Delete(service);
        await serviceInfoRepository.SaveAsync();

        return true;
    }
}
