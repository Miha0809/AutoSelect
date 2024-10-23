using AutoSelect.API.Models;
using AutoSelect.API.Models.DTOs.Requests;
using AutoSelect.API.Models.Enums;
using AutoSelect.API.Repositories.Interfaces;
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
    /// Профіль користувача.
    /// </summary>
    /// <param name="email">Електронна пошта користувача.</param>
    async Task<TUser> IProfileService.ProfileAsync<TUser>(string email)
    {
        var user = await userSearchRepository.GetUserByEmailAsync<TUser>(email);
        return user!;
    }

    /// <summary>
    /// TODO: name
    /// </summary>
    /// <param name="userUpdate"></param>
    /// <param name="email"></param>
    /// <typeparam name="TUser"></typeparam>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    async Task<TUser> IProfileService.UpdateAsync<TUser>(TUser userUpdate, string email)
    {
        var user = await userSearchRepository.GetUserByEmailAsync<TUser>(email);
        
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user), "User is null");
        }

        user = userUpdate;
        
        userRepository.Update<TUser>(user);
        userRepository.Save();

        return (await userSearchRepository.GetUserByEmailAsync<TUser>(email))!;
    }


    /// <summary>
    /// Видалити профіль.
    /// </summary>
    /// <param name="email">Електронна пошта користувача.</param>
    async Task<bool> IProfileService.DeleteAsync<TUser>(string email)
    {
        var user = await userSearchRepository.GetUserByEmailAsync<TUser>(email);

        if (user is null)
        {
            throw new ArgumentNullException(nameof(user), "User is null");
        }

        userRepository.Remove(user!);
        userRepository.Save();

        var isExistsUser = await userSearchRepository.GetUserByEmailAsync<TUser>(email) is null;

        return isExistsUser;
    }

    /// <summary>
    /// Редагування профілю користувача після першої авторизації.
    /// </summary>
    /// <param name="updateProfileDto">Оновленні дані.</param>
    /// <param name="email">Електронна пошта користувача.</param>
    async Task<TUser?> IProfileService.UpdateAfterFirstLoginAsync<TUser>(
        UpdateProfileAfterFirstLoginDto updateProfileDto,
        string email
    ) where TUser : class
    {
        var user = await userSearchRepository.GetUserByEmailAsync<TUser>(email);

        if (user is not null && user.FirstName is null && user.LastName is null)
        {
            user!.FirstName ??= updateProfileDto.FirstName;
            user!.LastName ??= updateProfileDto.LastName;

            var userRoles = await userManager.GetRolesAsync(user);

            if (userRoles.Count == 0 && updateProfileDto.IsExpert)
            {
                await userManager.AddToRoleAsync(user, nameof(Roles.Expert));

                var expert = new Expert
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                userRepository.Add(expert);
            }
            else if (userRoles.Count == 0 && !updateProfileDto.IsExpert)
            {
                await userManager.AddToRoleAsync(user, nameof(Roles.Client));

                var client = new Client
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                userRepository.Add(client);
            }

            // userRepository.Update(user);
            userRepository.Save();

            return user;
        }

        throw new ArgumentException(nameof(user), "User is changed");
    }
}
