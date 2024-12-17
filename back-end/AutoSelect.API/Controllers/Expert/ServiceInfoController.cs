using AutoSelect.API.Contexts;
using AutoSelect.API.Models.DTOs.Responses.Expert;
using AutoSelect.API.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoSelect.API.Controllers.Expert;

/// <summary>
/// 
/// </summary>
/// <param name="context">asd</param>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(Roles.Expert))]
public class ServiceInfoController(AutoSelectDbContext context) : Controller
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<IActionResult> Information()
    {
        var email = User.Identity!.Name!;
        var user = context.Experts.FirstOrDefault(s => s.Email!.Equals(email))!.ServiceInfos;

        return Task.FromResult<IActionResult>(Ok(user));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ServiceInfoDto dto)
    {
        var email = User.Identity!.Name!;
        var user = await context.Experts.FirstOrDefaultAsync(s => s.Email!.Equals(email));
        
        // user!.ServiceInfos.Add(dto);

        return Ok(user);
    }
}