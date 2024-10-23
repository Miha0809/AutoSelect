using AutoMapper;
using AutoSelect.API.Models.DTOs.Responses.Expert;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoSelect.API.Controllers.Expert;

/// <summary>
/// Контроллер профілю експерта.
/// </summary>
/// <param name="service">Сервіс профілю користувача.</param>
/// <param name="userSearchRepository">Сервіс профілю користувача.</param>
/// <param name="mapper">Маппер об'єктів.</param>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileExpertController(IProfileService service, IUserSearchRepository userSearchRepository, IMapper mapper) : Controller
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
            var user = await service.ProfileAsync<Models.Expert>(email);

            return Ok(mapper.Map<ExpertPrivateShowDto>(user));
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] int price)
    {
        var email = User.Identity!.Name!;
        var user = await userSearchRepository.GetUserByEmailAsync<Models.Expert>(email);
        user!.Price = price;
        await service.UpdateAsync<Models.Expert>(user, email);
        var user2 = await userSearchRepository.GetUserByEmailAsync<Models.Expert>(email);

        return Ok(user2);
    }
}
