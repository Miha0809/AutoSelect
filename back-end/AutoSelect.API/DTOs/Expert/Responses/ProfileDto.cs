using AutoSelect.API.DTOs.Expert.Responses;

namespace AutoSelect.API.DTOs.Expert.Responses;

/// <summary>
/// DTO для приватної та публічної інформації про експерта.
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
  
  /// <summary>
  /// Послуги експерта.
  /// </summary>
  public IEnumerable<ServiceInfoDto>? ServiceInfo { get; set; }
}
