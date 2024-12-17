namespace AutoSelect.API.Models.DTOs.Responses;

/// <summary>
/// Dto для інформації після першого входу користувача.
/// </summary>
public class UserInfoAfterFirstLoginDto
{
    /// <summary>
    /// Електронна пошта користувача.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Ім'я.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Прізвище.
    /// </summary>
    public required string LastName { get; set; }
}