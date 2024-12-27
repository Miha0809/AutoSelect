using AutoSelect.API.DTOs.Expert.Responses;
using AutoSelect.API.Models.Expert;
using AutoSelect.API.Models.User;

namespace AutoSelect.API.Services.Interfaces;

/// <summary>
/// Інтерфейс сервіса послуг експерта.
/// </summary>
public interface IServiceInfoService
{
    /// <summary>
    /// Всі послуги конкретного експерта.
    /// </summary>
    Task<IEnumerable<ServiceInfo>?> GetOwnerServicesAsync(string email);

    /// <summary>
    /// Добавити нову послугу.
    /// </summary>
    /// <param name="serviceInfo">Послуга.</param>
    /// <param name="email">Електрона пошта власника послугию.</param>
    Task<ServiceInfo> AddAsync<TUser>(ServiceInfo serviceInfo, string email) where TUser : User;

    /// <summary>
    /// Редагування послуги.
    /// </summary>
    /// <param name="serviceInfoUpdateDto">Послуга з новими даними.</param>
    Task<ServiceInfo> UpdateAsync(ServiceInfoDto serviceInfoUpdateDto);

    /// <summary>
    /// Видалення конкретної послуги.
    /// </summary>
    /// <param name="id">Ідентифікатор послуги.</param>
    Task<bool> DeleteAsync(int id);
}
