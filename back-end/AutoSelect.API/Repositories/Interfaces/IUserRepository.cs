using AutoSelect.API.Models.User;

namespace AutoSelect.API.Repositories.Interfaces;

/// <summary>
/// Інтерфейс репозіторія користувача.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Всі користувачі.
    /// </summary>
    Task<IEnumerable<TUser>> GetAllUsersAsync<TUser>() where TUser : User;

    /// <summary>
    /// Користувач по елекронній пошті.
    /// </summary>
    /// <param name="email">Електронна пошта.</param>
    Task<TUser?> GetUserByEmailAsync<TUser>(string email) where TUser : User;

    /// <summary>
    /// Створити нового користувача.
    /// </summary>
    /// <param name="user">Користувач.</param>
    void Add<TUser>(TUser user) where TUser : User;

    /// <summary>
    /// Видалення користувача.
    /// </summary>
    /// <param name="user">Користувач.</param>
    void Remove<TUser>(TUser user) where TUser : User;

    /// <summary>
    /// Збереження змін.
    /// </summary>
    Task SaveAsync();
}
