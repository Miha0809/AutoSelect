using AutoSelect.API.Contexts;
using AutoSelect.API.Repositories.Interfaces;
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
    async Task<TUser?> IUserSearchRepository.GetUserByEmailAsync<TUser>(string email) where TUser : class
    {
        var userByEmail = await context.Set<TUser>().FirstOrDefaultAsync(user =>
            user.Email!.Equals(email)
        );

        return userByEmail;
    }

    /// <summary>
    /// Всі користувачі.
    /// </summary>
    List<TUser> IUserSearchRepository.GetUsers<TUser>()
    {
        return context.Set<TUser>().ToList();
    }
}
