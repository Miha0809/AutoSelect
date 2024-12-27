using AutoMapper;
using AutoSelect.API.DTOs.Expert.Responses;
using AutoSelect.API.Models.Enums;
using AutoSelect.API.Models.Expert;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoSelect.API.Controllers.Expert;

/// <summary>
/// Контроллер для CRUD операцій для послуг експерта.
/// </summary>
/// <param name="serviceInfoService">Сервіс для CRUD операцій для послуг експерта.</param>
/// <param name="mapper">Маппер об'єктів.</param>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(Roles.Expert))]
public class ServiceInfoController(IServiceInfoService serviceInfoService, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Послуги експерта.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Services()
    {
        try
        {
            var email = User.Identity!.Name!;
            var services = await serviceInfoService.GetOwnerServicesAsync(email);

            return Ok(mapper.Map<IEnumerable<ServiceInfo>?, List<ServiceInfoDto>>(services));
        }
        catch (Exception)
        {
            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }

    /// <summary>
    /// Добавити послугу.
    /// </summary>
    /// <param name="serviceInfoDto">Послуга.</param>
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ServiceInfoDto serviceInfoDto)
    {
        try
        {
            var email = User.Identity!.Name!;
            var service = await serviceInfoService.AddAsync<Models.Expert.Expert>(mapper.Map<ServiceInfo>(serviceInfoDto), email);

            return Ok(mapper.Map<ServiceInfoDto>(service));
        }
        catch (Exception)
        {
            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }

    /// <summary>
    /// Редагувати послугу.
    /// </summary>
    /// <param name="dto">Послуга із новими даними.</param>
    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] ServiceInfoDto dto)
    {
        try
        {
            var service = await serviceInfoService.UpdateAsync(dto);

            return Ok(mapper.Map<ServiceInfoDto>(service));
        }
        catch (Exception)
        {
            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }

    /// <summary>
    /// Видалення послуги.
    /// </summary>
    /// <param name="id">Ідентифікатор послуги, яку потрібно видалити.</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var isDeleted = await serviceInfoService.DeleteAsync(id);

            return Ok(isDeleted ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest);
        }
        catch (Exception)
        {
            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }
}