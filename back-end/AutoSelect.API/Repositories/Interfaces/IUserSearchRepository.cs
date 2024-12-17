using AutoSelect.API.Models;

namespace AutoSelect.API.Repositories.Interfaces;

/// <summary>
/// Інтерфейс репозіторі пошуку користувача.
/// </summary>
public interface IUserSearchRepository
{
    /// <summary>
    /// Користувач по елекронній пошті.
    /// </summary>
    /// <param name="email">Електронна пошта.</param>
    Task<TUser?> GetUserByEmailAsync<TUser>(string email) where TUser : User;

    /// <summary>
    /// Всі користувачі.
    /// </summary>
    List<TUser> GetUsers<TUser>() where TUser : User;
}
