using AutoSelect.API.Contexts;
using AutoSelect.API.Models;
using AutoSelect.API.Repositpries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoSelect.API.Repositories;

/// <summary>
/// Репозіторій для користувача.
/// </summary>
public class UserRepository(AutoSelectDbContext context) : IUserRepository
{
    /// <summary>
    /// Добавлення.
    /// </summary>
    /// <param name="user">Користувач.</param>
    public void Add<TUser>(TUser user) where TUser : User
    {
        context.Set<TUser>().Entry(user).State = EntityState.Deleted;
        context.Set<TUser>().Add(user);
    }

    /// <summary>
    /// Оновлення.
    /// </summary>
    /// <param name="user">Користувач.</param>
    void IUserRepository.Update<TUser>(TUser user)
    {
        context.Set<TUser>().Update(user);
    }

    /// <summary>
    /// Видалення.
    /// </summary>
    /// <param name="user">Користувач.</param>
    public void Remove<TUser>(TUser user) where TUser : User
    {
        context.Set<TUser>().Remove(user);
    }

    /// <summary>
    /// Збереження змін.
    /// </summary>
    void IUserRepository.Save()
    {
        context.SaveChanges();
    }
}
