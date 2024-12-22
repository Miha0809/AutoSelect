using System.ComponentModel.DataAnnotations;
using AutoSelect.API.DTOs.User.Requests;
using Microsoft.AspNetCore.Identity;

namespace AutoSelect.API.Models.User;

/// <summary>
/// Користувач.
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// Ім'я.
    /// </summary>
    [StringLength(16, MinimumLength = 2)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Прізвище.
    /// </summary>
    [StringLength(32, MinimumLength = 2)]
    public string? LastName { get; set; }

    /// <summary>
    /// конструктор без параметрів.
    /// </summary>
    public User() { }

    /// <summary>
    /// Копіюючий конструктор.
    /// </summary>
    /// <param name="user">Користувач з якого скопіюються дані.</param>
    public User(User user)
    {
        Id = user.Id;
        Email = user.Email;
        FirstName = user.FirstName;
        LastName = user.LastName;
    }

    /// <summary>
    /// Копіюючий конструктор.
    /// </summary>
    /// <param name="updateProfileDto">Користувач з якого скопіюються дані.</param>
    public User(UpdateProfileDto updateProfileDto)
    {
        FirstName = updateProfileDto.FirstName;
        LastName = updateProfileDto.LastName;
    }
}