using AutoMapper;
using AutoSelect.API.Models.DTOs.Requests;
using AutoSelect.API.Models.DTOs.Responses;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoSelect.API.Controllers;

/// <summary>
/// Контроллер профілю користувача.
/// </summary>
/// <param name="service">Сервіс профілю користувача.</param>
/// <param name="mapper">Маппер об'єктів.</param>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController(IProfileService service, IMapper mapper) : Controller
{
    /// <summary>
    /// Редагування данних користувача.
    /// </summary>
    /// <param name="updateProfileDto">Оновлені дані користувача.</param>
    [HttpPatch]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateProfileDto)
    {
        try
        {
            var email = User.Identity!.Name!;
            var updatedUser = await service.UpdateAsync(updateProfileDto, email);

            return Ok(mapper.Map<UserPrivateShowDto>(updatedUser));
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Профіль користувача.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        try
        {
            var email = User.Identity!.Name!;
            var user = await service.ProfileAsync(email);

            return Ok(user);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
