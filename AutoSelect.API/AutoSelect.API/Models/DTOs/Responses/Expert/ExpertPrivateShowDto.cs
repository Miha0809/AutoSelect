namespace AutoSelect.API.Models.DTOs.Responses.Expert;

/// <summary>
/// DTO для приватної інформації про експерта.
/// </summary>
public class ExpertPrivateShowDto
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
