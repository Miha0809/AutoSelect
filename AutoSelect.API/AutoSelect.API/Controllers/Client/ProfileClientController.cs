using AutoMapper;
using AutoSelect.API.Models.DTOs.Responses.Client;
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
[Authorize]
public class ProfileClientController(IProfileService service, IMapper mapper) : Controller
{
    /// <summary>
    /// Профіль користувача.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        try
        {
            var email = User.Identity!.Name!;
            var user = await service.ProfileAsync<Models.Client>(email);

            return Ok(mapper.Map<ClientPrivateShowDto>(user));
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
