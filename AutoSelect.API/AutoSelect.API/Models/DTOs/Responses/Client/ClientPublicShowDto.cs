namespace AutoSelect.API.Models.DTOs.Responses.Client;

/// <summary>
/// DTO для публічної інформації про експерта.
/// </summary>
public class ClientPublicShowDto
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
