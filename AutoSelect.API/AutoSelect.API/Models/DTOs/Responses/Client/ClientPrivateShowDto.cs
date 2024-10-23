namespace AutoSelect.API.Models.DTOs.Responses.Client;

/// <summary>
/// DTO для приватної інформації про клієнта.
/// </summary>
public class ClientPrivateShowDto
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
