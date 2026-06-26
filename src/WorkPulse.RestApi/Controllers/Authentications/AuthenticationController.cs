using Microsoft.AspNetCore.Mvc;
using WorkPulse.Services.Common.Interfaces.identity;
using WorkPulse.Services.Common.Interfaces.Security;
using WorkPulse.Services.Users.Contracts;
using WorkPulse.Services.Users.Contracts.DTOs.Request;
using WorkPulse.Services.Users.Contracts.DTOs.Response;
using WorkPulse.Services.Users.Exceptions;

namespace WorkPulse.RestApi.Controllers.Authentications;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationsController(
    IUserService service,
    IIdentityService passwordHasher,
    ITokenService tokenService): ControllerBase
{
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var user = await service.FindByUsername(dto.Username);
        if (user.IsExist && passwordHasher.VerifyPassword(user.Password!,
                dto.Password))
        {
            return Ok(tokenService.GenerateToken(new BaseUserInformationDto
            {
                Username = dto.Username,
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                Email = user.Email!,
                Role = user.Role,
                Id = user.UserId
            }));
        }
        else
        {
            return Unauthorized();
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]AddUserDto dto)
    {
        try
        {
            var response = await service.Add(dto);
            var jwt = tokenService.GenerateToken(response);
            return Ok(jwt);
        }
        catch (DuplicateUserException)
        {
            return Conflict($"Username '{dto.Username}' or email '{dto.Email}' already exists.");
        }
    }
        
}
