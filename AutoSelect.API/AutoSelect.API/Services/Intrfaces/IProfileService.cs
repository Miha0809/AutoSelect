using AutoSelect.API.Models;
using AutoSelect.API.Models.DTOs.Requests;

namespace AutoSelect.API.Services.Interfaces;

/// <summary>
/// Інтерфейс сервіса профілю користувача.
/// </summary>
public interface IProfileService
{
    /// <summary>
    /// Профіль користувача.
    /// </summary>
    /// <param name="email">Електронна пошта користувача.</param>
    Task<TUser> ProfileAsync<TUser>(string email) where TUser : User;

    /// <summary>
    /// TODO: name
    /// </summary>
    /// <param name="userUpdate"></param>
    /// <param name="email"></param>
    Task<TUser> UpdateAsync<TUser>(TUser userUpdate, string email) where TUser : User;
    
    /// <summary>
    /// Редагування профілю користувача.
    /// </summary>
    /// <param name="updateProfileDto">Оновленні дані.</param>
    /// <param name="email">Електронна пошта користувача.</param>
    Task<TUser?> UpdateAfterFirstLoginAsync<TUser>(
        UpdateProfileAfterFirstLoginDto updateProfileDto,
        string email
    ) where TUser : User;

    /// <summary>
    /// Видалити профіль.
    /// </summary>
    /// <param name="email">Електронна пошта користувача.</param>
    Task<bool> DeleteAsync<TUser>(string email) where TUser : User;
}
