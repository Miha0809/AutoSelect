using AutoSelect.API.Models.Expert;

namespace AutoSelect.API.Repositories.Interfaces;

/// <summary>
/// Інтерфейс репозіторій для CRUD операцій для послуг експерта.
/// </summary>
public interface IServiceInfoRepository
{
    /// <summary>
    /// Отримати всі послуги конкретного експерта.
    /// </summary>
    /// <param name="email">Електрона пошта експерта.</param>
    Task<IEnumerable<ServiceInfo>> GetAllServiceInfosAsync(string email);

    /// <summary>
    /// Отримати послугу по ідентифікатору.
    /// </summary>
    /// <param name="id">Ідентифікатор послуги.</param>
    Task<ServiceInfo?> GetServiceInfoByIdAsync(int id);

    /// <summary>
    /// Добавити нову послугу.
    /// </summary>
    /// <param name="service">Послуга.</param>
    Task AddAsync(ServiceInfo service);

    /// <summary>
    /// Видалити послугу.
    /// </summary>
    /// <param name="serviceInfo">Послуга.</param>
    void Delete(ServiceInfo serviceInfo);

    /// <summary>
    /// Зберегти зміни у БД.
    /// </summary>
    Task SaveAsync();
}
