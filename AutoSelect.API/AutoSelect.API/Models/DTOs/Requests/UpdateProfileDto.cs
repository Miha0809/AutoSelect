namespace AutoSelect.API.Models.DTOs.Requests;

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

    /// <summary>
    /// Чи є користувач експертом.
    /// </summary>
    /// <example>false</example>
    public bool IsExpert { get; set; }
}
