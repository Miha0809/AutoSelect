using AutoSelect.API.Models.User;
using AutoSelect.API.DTOs.User.Requests;

namespace AutoSelect.API.Services.Interfaces;

/// <summary>
/// Інтерфейс сервіса профілю користувача.
/// </summary>
public interface IProfileService
{
    /// <summary>
    /// Всі користувачі.
    /// </summary>
  	Task<IEnumerable<TUser>> GetAllProfilesAsync<TUser>() where TUser : User;

    /// <summary>
    /// Профіль певного користувача.
    /// </summary>
    /// <param name="email">Електронна пошта користувача.</param>
    Task<TUser> GetProfileAsync<TUser>(string email) where TUser : User;

    /// <summary>
    /// Редагування профілю користувача.
    /// </summary>
    /// <param name="updateProfileDto">Оновленні дані.</param>
    /// <param name="email">Електронна пошта користувача.</param>
    Task UpdateAfterFirstLoginAsync<TUser, TUpdate>(
        TUpdate updateProfileDto,
        string email
    ) where TUser : User
      where TUpdate : UpdateProfileDto;

    /// <summary>
    /// Видалити профіль.
    /// </summary>
    /// <param name="email">Електронна пошта користувача.</param>
    Task<bool> DeleteAsync<TUser>(string email) where TUser : User;
}
