namespace AutoSelect.API.DTOs.User.Requests;

/// <summary>
/// DTO редагування профілю користувача.
/// </summary>
public class UpdateProfileDto
{
    /// <summary>
    /// Ім'я.
    /// </summary>
    /// <example>Mark</example>
    public required string FirstName { get; set; }

    /// <summary>
    /// Прізвище.
    /// </summary>
    /// <example>Full</example>
    public required string LastName { get; set; }
}
