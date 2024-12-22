using AutoMapper;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoSelect.API.DTOs.User.Requests;

namespace AutoSelect.API.Controllers.User;

/// <summary>
/// Контроллер профілю для всіх користувачів.
/// </summary>
/// <param name="service">Сервіс профілю користувача.</param>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController(IProfileService service) : ControllerBase
{
    /// <summary>
    /// Редагування данних користувача після першої атворизації.
    /// </summary>
    /// <param name="updateProfileDto">Оновлені дані користувача.</param>
    [HttpPatch]
    public async Task<IActionResult> UpdateProfile(
        [FromBody] UpdateProfileAfterFirstLoginDto updateProfileDto
    )
    {
        try
        {
            var email = User.Identity!.Name!;
            await service.UpdateAfterFirstLoginAsync<Models.User.User, UpdateProfileAfterFirstLoginDto>(updateProfileDto, email);

            return Ok(StatusCodes.Status200OK);
        }
        catch (Exception)
        {
            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }

    /// <summary>
    /// Вийти з акаунта.
    /// </summary>
    [HttpDelete("logout")]
    public IActionResult Logout()
    {
        try
        {
            RemoveIdentityCookies();

            return Ok(StatusCodes.Status200OK);
        }
        catch (Exception)
        {
            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }

    /// <summary>
    /// Видалення акаунта.
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        try
        {
            var email = User.Identity!.Name!;
            var isDeletedUser = await service.DeleteAsync<Models.User.User>(email);

            RemoveIdentityCookies();

            return Ok(isDeletedUser);
        }
        catch (Exception)
        {
            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }

    private void RemoveIdentityCookies()
    {
        HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
    }
}