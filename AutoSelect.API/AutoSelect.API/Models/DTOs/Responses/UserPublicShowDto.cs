namespace AutoSelect.API.Models.DTOs.Responses;

/// <summary>
/// DTO для приватної інформації про користувача.
/// </summary>
public class UserPublicShowDto
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
