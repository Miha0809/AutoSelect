using AutoSelect.API.Context;
using AutoSelect.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoSelect.API.Repositories;

/// <summary>
/// Репозіторій користувача.
/// </summary>
/// <param name="context">Контекст БД.</param>
public class UserRepository(AutoSelectDbContext context) : IUserRepository
{
    /// <summary>
    /// Всі користувачі.
    /// </summary>
    async Task<IEnumerable<TUser>> IUserRepository.GetAllUsersAsync<TUser>()
    {
        var users = await context.Set<TUser>().ToListAsync();

        return users;
    }

    /// <summary>
    /// Користувач по елекронній пошті.
    /// </summary>
    /// <param name="email">Електронна пошта.</param>
    async Task<TUser?> IUserRepository.GetUserByEmailAsync<TUser>(string email) where TUser : class
    {
        var userByEmail = await context.Set<TUser>().FirstOrDefaultAsync(user =>
            user!.Email!.Equals(email)
        );

        return userByEmail;
    }

    /// <summary>
    /// Створити нового користувача.
    /// </summary>
    /// <param name="user">Користувач.</param>
    void IUserRepository.Add<TUser>(TUser user)
    {
        context.Entry(user).State = EntityState.Deleted;
        context.Set<TUser>().Add(user);
    }

    /// <summary>
    /// Видалення користувача.
    /// </summary>
    /// <param name="user">Користувач.</param>
    void IUserRepository.Remove<TUser>(TUser user)
    {
        context.Set<TUser>().Remove(user);
    }

    /// <summary>
    /// Збереження змін.
    /// </summary>
    async Task IUserRepository.SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
