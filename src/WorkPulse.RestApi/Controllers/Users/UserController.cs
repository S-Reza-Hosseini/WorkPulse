using Microsoft.AspNetCore.Mvc;
using WorkPulse.Services.Users.Contracts;
using WorkPulse.Services.Users.Contracts.DTOs.Request;

namespace WorkPulse.RestApi.Controllers.Users;
[ApiController]
[Route("users")]
public class UserController(IUserService service): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add(AddUserDto dto)
    {
        await service.Add(dto);
        return Ok();
    }
}