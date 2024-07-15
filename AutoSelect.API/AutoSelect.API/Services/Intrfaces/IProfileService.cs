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
    Task<User> ProfileAsync(string email);

    /// <summary>
    /// Редагування профілю користувача.
    /// </summary>
    /// <param name="updateProfileDto">Оновленні дані.</param>
    /// <param name="email">Електронна пошта користувача.</param>
    Task<User?> UpdateAfterFirstLoginAsync(
        UpdateProfileAfterFirstLoginDto updateProfileDto,
        string email
    );

    /// <summary>
    /// Видалити профіль.
    /// </summary>
    /// <param name="email">Електронна пошта користувача.</param>
    Task<bool> DeleteAsync(string email);
}
