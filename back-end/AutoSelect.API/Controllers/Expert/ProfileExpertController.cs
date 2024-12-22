using AutoMapper;
using AutoSelect.API.DTOs.Expert.Responses;
using AutoSelect.API.Models.Enums;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoSelect.API.Controllers.Expert;

/// <summary>
/// Контроллер профілю експерта.
/// </summary>
/// <param name="profileService">Сервіс профілю користувача.</param>
/// <param name="mapper">Маппер об'єктів.</param>
[ApiController]
[Route("api/[controller]")]
public class ProfileExpertController(IProfileService profileService, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Профіль експерта.
    /// </summary>
    [HttpGet("private")]
    [Authorize(Roles = nameof(Roles.Expert))]
    public async Task<IActionResult> Profile()
    {
        try
        {
            var email = User.Identity!.Name!;
            var user = await profileService.GetProfileAsync<Models.Expert.Expert>(email);

            return Ok(mapper.Map<ProfileDto>(user));
        }
        catch (Exception)
        {
            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }

    /// <summary>
    /// Всі експерти.
    /// </summary>
    [HttpGet("experts")]
    public async Task<IActionResult> Profiles()
    {
        try
        {
            var experts = await profileService.GetAllProfilesAsync<Models.Expert.Expert>();

            return Ok(mapper.Map<IEnumerable<Models.Expert.Expert>, List<ProfileDto>>(experts));
        }
        catch (Exception)
        {
            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }

    /// <summary>
    /// Конкретний експерт по електроній пошті.
    /// </summary>
    /// <param name="email">Електрона пошта експерта.</param>
    [HttpGet("public")]
    public async Task<IActionResult> Profile(string email)
    {
        try
        {
            var expert = await profileService.GetProfileAsync<Models.Expert.Expert>(email);

            return Ok(new
            {
                expert = mapper.Map<ProfileDto>(expert)
            });
        }
        catch (Exception)
        {
            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }
}
