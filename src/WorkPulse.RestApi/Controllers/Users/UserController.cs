using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPulse.RestApi.Auth;
using WorkPulse.Services.Users.Contracts;
using WorkPulse.Services.Users.Contracts.DTOs.Request;
using WorkPulse.Services.Users.Contracts.DTOs.Response;

namespace WorkPulse.RestApi.Controllers.Users;
[ApiController]
[Route("users")]
[Authorize]
public class UserController(
    IUserService service,
    IUserQuery query): ControllerBase
{
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }
        await service.Update(userId, dto);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<List<GetAllUserDto>> GetAll()
    {
        return await query.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]string id)
    {
        var user = await query.GetById(id);
        
        if (user is null)
        {
            return NotFound($"User with ID '{id}' not found.");
        }

        return Ok(user);
    }
}