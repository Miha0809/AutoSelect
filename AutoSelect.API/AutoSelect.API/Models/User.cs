using Microsoft.AspNetCore.Identity;

namespace AutoSelect.API.Models;

/// <summary>
/// Користувач.
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// Ім'я.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Прізвище.
    /// </summary>
    public string? LastName { get; set; }
}
