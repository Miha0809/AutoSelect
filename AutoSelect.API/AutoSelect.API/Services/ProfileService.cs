using AutoSelect.API.Models;
using AutoSelect.API.Models.DTOs.Requests;
using AutoSelect.API.Models.Enums;
using AutoSelect.API.Repositpries.Interfaces;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AutoSelect.API.Services;

/// <summary>
/// Сервіс профілю користувача.
/// </summary>
/// <param name="userRepository">Репозіторі користувача.</param>
/// <param name="userSearchRepository">Репозіторі пошуку користувача.</param>
/// <param name="userManager">Менеджер Identity користувача.</param>
public class ProfileService(
    IUserRepository userRepository,
    IUserSearchRepository userSearchRepository,
    UserManager<User> userManager
) : IProfileService
{
    /// <summary>
    /// Видалити профіль.
    /// </summary>
    /// <param name="email">Електронна пошта користувача.</param>
    async Task<bool> IProfileService.DeleteAsync(string email)
    {
        var user = await userSearchRepository.GetUserByEmailAsync(email);
        userRepository.Remove(user!);
        return userSearchRepository.GetUserByEmailAsync(email) is null;
    }

    /// <summary>
    /// Профіль користувача.
    /// </summary>
    /// <param name="email">Електронна пошта користувача.</param>
    async Task<User> IProfileService.ProfileAsync(string email)
    {
        var user = await userSearchRepository.GetUserByEmailAsync(email);
        return user!;
    }

    /// <summary>
    /// Редагування профілю користувача після першої авторизації.
    /// </summary>
    /// <param name="updateProfileDto">Оновленні дані.</param>
    /// <param name="email">Електронна пошта користувача.</param>
    async Task<User?> IProfileService.UpdateAfterFirstLoginAsync(
        UpdateProfileAfterFirstLoginDto updateProfileDto,
        string email
    )
    {
        var user = await userSearchRepository.GetUserByEmailAsync(email);

        if (user is not null && user.FirstName is null && user.LastName is null)
        {
            user!.FirstName ??= updateProfileDto.FirstName;
            user!.LastName ??= updateProfileDto.LastName;

            var userRoles = await userManager.GetRolesAsync(user);

            if (userRoles.Count == 0 && updateProfileDto.IsExpert)
            {
                await userManager.AddToRoleAsync(user, nameof(Roles.Expert));
            }
            else if (userRoles.Count == 0 && !updateProfileDto.IsExpert)
            {
                await userManager.AddToRoleAsync(user, nameof(Roles.Client));
            }

            userRepository.Update(user!);
            userRepository.Save();

            return user;
        }

        return null;
    }
}
