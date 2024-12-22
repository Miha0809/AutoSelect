using AutoMapper;
using AutoSelect.API.Models.Expert;
using AutoSelect.API.DTOs.Expert.Responses;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services.Interfaces;

namespace AutoSelect.API.Services;

/// <summary>
/// Сервіс послуг експерта.
/// </summary>
/// <param name="serviceInfoRepository">TODO: name</param>
/// <param name="userRepository">TODO: name</param>
/// <param name="mapper">TODO: name</param>
public class ServiceInfoService(IServiceInfoRepository serviceInfoRepository, IUserRepository userRepository, IMapper mapper) : IServiceInfoService
{
    /// <summary>
    /// Конкретний послуга по ідентифікатору.
    /// </summary>
    /// <param name="id">Ідентифікатор послуги.</param>
    async Task<ServiceInfo?> IServiceInfoService.ServiceInfoByIdAsync(int id)
    {
        var service = await serviceInfoRepository.GetServiceInfoByIdAsync(id);

        return service;
    }

    /// <summary>
    /// Всі послуги конкретного експерта.
    /// </summary>
    async Task<IEnumerable<ServiceInfo>> IServiceInfoService.GetAllServiceInfosAsync(string email)
    {
        var services = await serviceInfoRepository.GetAllServiceInfosAsync(email);

        return services;
    }

    /// <summary>
    /// Добавити нову послугу.
    /// </summary>
    /// <param name="serviceInfo">Послуга.</param>
    /// <param name="email">Електрона пошта власника послугию.</param>
    async Task<ServiceInfo> IServiceInfoService.AddAsync<TUser>(ServiceInfo serviceInfo, string email)
    {
        var owner = await userRepository.GetUserByEmailAsync<TUser>(email);
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

        if (service is not null)
        {
            mapper.Map(serviceInfoUpdateDto, service);
            await serviceInfoRepository.SaveAsync();

            return (await serviceInfoRepository.GetServiceInfoByIdAsync(serviceInfoUpdateDto.Id))!;
        }

        throw new ArgumentException();
    }

    /// <summary>
    /// Видалення конкретної послуги.
    /// </summary>
    /// <param name="id">Ідентифікатор послуги.</param>
    async Task<bool> IServiceInfoService.DeleteAsync(int id)
    {
        var service = await serviceInfoRepository.GetServiceInfoByIdAsync(id);

        if (service is not null)
        {
            serviceInfoRepository.Delete(service);
            await serviceInfoRepository.SaveAsync();

            return true;
        }

        return false;
    }
}
