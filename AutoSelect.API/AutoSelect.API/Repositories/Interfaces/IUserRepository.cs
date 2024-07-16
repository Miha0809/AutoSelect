using AutoSelect.API.Models;

namespace AutoSelect.API.Repositpries.Interfaces;

/// <summary>
/// Інтерфейс репозіторія користувача.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Оновлення.
    /// </summary>
    /// <param name="user">Користувач.</param>
    void Update(User user);

    /// <summary>
    /// Видалення.
    /// </summary>
    /// <param name="user">Користувач.</param>
    void Remove(User user);

    /// <summary>
    /// Збереження змін.
    /// </summary>
    void Save();
}
