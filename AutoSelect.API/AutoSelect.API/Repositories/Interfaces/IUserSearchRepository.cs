using AutoSelect.API.Models;

namespace AutoSelect.API.Repositpries.Interfaces;

/// <summary>
/// Інтерфейс репозіторі пошуку користувача.
/// </summary>
public interface IUserSearchRepository
{
  /// <summary>
  /// Користувач по елекронній пошті.
  /// </summary>
  /// <param name="email">Електронна пошта.</param>
  Task<User?> GetUserByEmailAsync(string email);

  /// <summary>
  /// Всі користувачі.
  /// </summary>
  List<User> GetUsers();
}
