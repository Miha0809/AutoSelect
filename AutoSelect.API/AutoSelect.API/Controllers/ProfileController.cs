using AutoMapper;
using AutoSelect.API.Models;
using AutoSelect.API.Models.DTOs.Requests;
using AutoSelect.API.Models.DTOs.Responses.Expert;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoSelect.API.Controllers;

/// <summary>
/// Контроллер профілю для всіх користувачів.
/// </summary>
/// <param name="service">Сервіс профілю користувача.</param>
/// <param name="mapper">Маппер об'єктів.</param>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController(IProfileService service, IMapper mapper) : Controller
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
            var updatedUser = await service.UpdateAfterFirstLoginAsync<User>(updateProfileDto, email);

            return Ok(mapper.Map<ExpertPrivateShowDto>(updatedUser));
        }
        catch (System.Exception)
        {
            throw;
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
            HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");

            return Ok(StatusCodes.Status200OK);
        }
        catch (System.Exception)
        {
            throw;
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
            var isDeletedUser = await service.DeleteAsync<User>(email);

            HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");

            return Ok(isDeletedUser);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}