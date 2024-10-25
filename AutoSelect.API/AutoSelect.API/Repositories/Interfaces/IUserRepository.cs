using AutoSelect.API.Models;

namespace AutoSelect.API.Repositories.Interfaces;

/// <summary>
/// Інтерфейс репозіторія користувача.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Добавлення.
    /// </summary>
    /// <param name="user">Користувач.</param>
    void Add<TUser>(TUser user) where TUser : User;
    
    /// <summary>
    /// Оновлення.
    /// </summary>
    /// <param name="user">Користувач.</param>
    void Update<TUser>(TUser user) where TUser : User;

    /// <summary>
    /// Видалення.
    /// </summary>
    /// <param name="user">Користувач.</param>
    void Remove<TUser>(TUser user) where TUser : User;

    /// <summary>
    /// Збереження змін.
    /// </summary>
    void Save();
}
