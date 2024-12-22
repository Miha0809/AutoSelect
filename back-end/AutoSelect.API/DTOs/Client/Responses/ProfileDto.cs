namespace AutoSelect.API.DTOs.Client.Responses;

/// <summary>
/// DTO для приватної та публічної інформації про клієнта.
/// </summary>
public class ProfileDto
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
