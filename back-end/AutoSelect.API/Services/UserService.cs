using AutoSelect.API.Models.User;
using AutoSelect.API.Models.Client;
using AutoSelect.API.Models.Enums;
using AutoSelect.API.Models.Expert;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using AutoSelect.API.DTOs.User.Requests;

namespace AutoSelect.API.Services;

/// <summary>
/// Сервіс профілю користувача.
/// </summary>
/// <param name="userRepository">Репозіторі користувача.</param>
/// <param name="userManager">Менеджер Identity користувача.</param>
/// <param name="mapper">Маппер об'єктів</param>
public class UserService(
    IUserRepository userRepository,
    UserManager<User> userManager,
    IMapper mapper
) : IUserService
{
    /// <summary>
    /// Всі користувачі.
    /// </summary>
    async Task<IEnumerable<TUser>> IUserService.GetAllUsersAsync<TUser>()
    {
        var users = await userRepository.GetAllUsersAsync<TUser>();

        return users;
    }

    /// <summary>
    /// Профіль користувача.
    /// </summary>
    /// <param name="email">Електронна пошта користувача.</param>
    async Task<TUser> IUserService.GetProfileAsync<TUser>(string email)
    {
        var user = await userRepository.GetUserByEmailAsync<TUser>(email);
        return user!;
    }

    /// <summary>
    /// Видалити профіль.
    /// </summary>
    /// <param name="email">Електронна пошта користувача.</param>
    async Task<bool> IUserService.DeleteAsync<TUser>(string email)
    {
        var user = await userRepository.GetUserByEmailAsync<TUser>(email);

        if (user is null)
        {
            throw new ArgumentNullException(nameof(user), "User is null");
        }

        userRepository.Remove(user);
        await userRepository.SaveAsync();

        var isDeletedUser = await userRepository.GetUserByEmailAsync<TUser>(email) is null;

        return isDeletedUser;
    }

    /// <summary>
    /// Редагування профілю користувача після першої авторизації.
    /// </summary>
    /// <param name="updateProfileDto">Оновленні дані.</param>
    /// <param name="email">Електронна пошта користувача.</param>
    async Task IUserService.UpdateAsync<TUser, TUpdate>(
        TUpdate updateProfileDto,
        string email
    )
    {
        var user = await userRepository.GetUserByEmailAsync<TUser>(email);

        mapper.Map(updateProfileDto, user);
        await userManager.UpdateAsync(user!);

        var userRoles = await userManager.GetRolesAsync(user!);

        if (userRoles is not null && userRoles.Count == 0)
        {
            if (updateProfileDto is UpdateProfileAfterFirstLoginDto update && update.IsExpert)
            {
                var expert = new Expert(user!);

                userRepository.Add(expert);
                await userManager.AddToRoleAsync(expert, nameof(Roles.Expert));
            }
            else
            {
                var client = new Client(user!);

                userRepository.Add(client);
                await userManager.AddToRoleAsync(client, nameof(Roles.Client));
            }
        }

        await userRepository.SaveAsync();
    }
}
