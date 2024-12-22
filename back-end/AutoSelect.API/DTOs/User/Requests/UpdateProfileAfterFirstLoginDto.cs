namespace AutoSelect.API.DTOs.User.Requests;

/// <summary>
/// DTO для редагування даних після першого входу.
/// </summary>
public class UpdateProfileAfterFirstLoginDto : UpdateProfileDto
{
    /// <summary>
    /// Чи є користувач експертом.
    /// </summary>
    /// <example>false</example>
    public bool IsExpert { get; set; }
}
