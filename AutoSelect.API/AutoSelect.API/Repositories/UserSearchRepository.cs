using AutoSelect.API.Contexts;
using AutoSelect.API.Models;
using AutoSelect.API.Repositpries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoSelect.API.Repositpries;

/// <summary>
/// Репозіторі пошуку користувача.
/// </summary>
public class UserSearchRepository(AutoSelectDbContext context) : IUserSearchRepository
{
  /// <summary>
  /// Користувач по елекронній пошті.
  /// </summary>
  /// <param name="email">Електронна пошта.</param>
  async Task<User?> IUserSearchRepository.GetUserByEmailAsync(string email)
  {
    var userByEmail = await context.Users.FirstOrDefaultAsync(user =>
        user.Email!.Equals(email)
    );

    return userByEmail;
  }

  /// <summary>
  /// Всі користувачі.
  /// </summary>
  List<User> IUserSearchRepository.GetUsers()
  {
    return context.Users.ToList();
  }
}
