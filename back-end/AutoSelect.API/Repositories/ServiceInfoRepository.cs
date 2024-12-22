using AutoSelect.API.Context;
using AutoSelect.API.Models.Expert;
using AutoSelect.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoSelect.API.Repositories;

/// <summary>
/// Репозіторій для CRUD операцій для послуг експерта.
/// </summary>
/// <param name="context">Контекст БД.</param>
public class ServiceInfoRepository(AutoSelectDbContext context) : IServiceInfoRepository
{
    /// <summary>
    /// Отримати всі послуги конкретного експерта.
    /// </summary>
    /// <param name="email">Електрона пошта експерта.</param>
    async Task<IEnumerable<ServiceInfo>> IServiceInfoRepository.GetAllServiceInfosAsync(string email)
    {
        var services = await context.ServiceInfos.Where(service => service.Owner.Email!.Equals(email)).ToListAsync();

        return services;
    }

    /// <summary>
    /// Отримати послугу по ідентифікатору.
    /// </summary>
    /// <param name="id">Ідентифікатор послуги.</param>
    async Task<ServiceInfo?> IServiceInfoRepository.GetServiceInfoByIdAsync(int id)
    {
        var service = await context.ServiceInfos.FindAsync(id);

        return service;
    }

    /// <summary>
    /// Добавити нову послугу.
    /// </summary>
    /// <param name="service">Послуга.</param>
    async Task IServiceInfoRepository.AddAsync(ServiceInfo service)
    {
        await context.ServiceInfos.AddAsync(service);
    }

    /// <summary>
    /// Видалити послугу.
    /// </summary>
    /// <param name="serviceInfo">Послуга.</param>
    void IServiceInfoRepository.Delete(ServiceInfo serviceInfo)
    {
        context.ServiceInfos.Remove(serviceInfo);
    }

    /// <summary>
    /// Зберегти зміни у БД.
    /// </summary>
    async Task IServiceInfoRepository.SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
