using AutoMapper;
using AutoSelect.API.DTOs.Client.Responses;
using AutoSelect.API.Models.Enums;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoSelect.API.Controllers.Client;

/// <summary>
/// Контроллер профілю клієнта.
/// </summary>
/// <param name="service">Сервіс профілю користувача.</param>
/// <param name="mapper">Маппер об'єктів.</param>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(Roles.Client))]
public class ProfileClientController(IProfileService service, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Профіль клієнта.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        try
        {
            var email = User.Identity!.Name!;
            var user = await service.GetProfileAsync<Models.Client.Client>(email);

            return Ok(mapper.Map<ProfileDto>(user));
        }
        catch (Exception)
        {
            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }
}
