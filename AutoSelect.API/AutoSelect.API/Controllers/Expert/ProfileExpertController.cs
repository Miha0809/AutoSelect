using AutoMapper;
using AutoSelect.API.Models;
using AutoSelect.API.Models.DTOs.Responses.Expert;
using AutoSelect.API.Models.Enums;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoSelect.API.Controllers.Expert;

/// <summary>
/// Контроллер профілю експерта.
/// </summary>
/// <param name="service">Сервіс профілю користувача.</param>
/// <param name="mapper">Маппер об'єктів.</param>
/// <param name="userManager">Маппер об'єктів.</param>
[ApiController]
[Route("api/[controller]")]
// [Authorize(Roles = $"Expert,EXPERT")]
[Authorize]
public class ProfileExpertController(IProfileService service, IMapper mapper, UserManager<User> userManager) : Controller
{
    /// <summary>
    /// Профіль експерта.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var email = User.Identity!.Name!;
        var user = await service.ProfileAsync<Models.Expert.Expert>(email);

        return Ok(mapper.Map<ExpertPrivateShowDto>(user));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost("role")]
    [AllowAnonymous]
    public async Task<IActionResult> ProfileExpert()
    {
        var user = await userManager.GetUserAsync(User);
        var roles = await userManager.GetUsersInRoleAsync(nameof(Roles.Expert));
        var a = await userManager.GetRolesAsync(user!);
        return Ok(new {
            user = user,
            roles = roles,
            a = a
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Expert")]
    public async Task<IActionResult> Test()
    {
        var user = await userManager.GetUserAsync(User);
        var roles = await userManager.GetRolesAsync(user!);

        return Ok(new
        {
            user = user,
            roles = roles
        });
    }
}
