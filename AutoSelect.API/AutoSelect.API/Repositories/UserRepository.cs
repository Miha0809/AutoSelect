using AutoSelect.API.Contexts;
using AutoSelect.API.Models;
using AutoSelect.API.Repositpries.Interfaces;

namespace AutoSelect.API.Repositories;

/// <summary>
/// Репозіторій для користувача.
/// </summary>
public class UserRepository(AutoSelectDbContext context) : IUserRepository
{
    /// <summary>
    /// Оновлення.
    /// </summary>
    /// <param name="user">Користувач.</param>
    void IUserRepository.Update(User user)
    {
        context.Users.Update(user);
    }

    /// <summary>
    /// Видалення.
    /// </summary>
    /// <param name="user">Користувач.</param>
    void IUserRepository.Remove(User user)
    {
        context.Users.Remove(user);
    }

    /// <summary>
    /// Збереження змін.
    /// </summary>
    void IUserRepository.Save()
    {
        context.SaveChanges();
    }
}
