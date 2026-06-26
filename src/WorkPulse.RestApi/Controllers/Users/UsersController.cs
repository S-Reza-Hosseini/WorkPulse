using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPulse.RestApi.Auth;
using WorkPulse.Services.Users.Contracts;
using WorkPulse.Services.Users.Contracts.DTOs.Request;
using WorkPulse.Services.Users.Contracts.DTOs.Response;
using WorkPulse.Services.Users.Exceptions;

namespace WorkPulse.RestApi.Controllers.Users;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController(
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

        try
        {
            await service.Update(userId, dto);
            return Ok();
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
        catch (DuplicateUserException)
        {
            return Conflict($"Username '{dto.Username}' or email '{dto.Email}' already exists.");
        }
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var userId = HttpContext.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }

        if (userId != id && !User.IsInRole("admin"))
        {
            return Forbid();
        }

        try
        {
            await service.Delete(id);
            return Ok();
        }
        catch (UserNotFoundException)
        {
            return NotFound($"User with ID '{id}' not found.");
        }
    }
}