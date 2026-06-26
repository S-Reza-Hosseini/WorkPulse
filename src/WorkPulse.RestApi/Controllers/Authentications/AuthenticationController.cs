using Microsoft.AspNetCore.Mvc;
using WorkPulse.Services.Authentications.Contracts;
using WorkPulse.Services.Authentications.Contracts.DTOs.Requests;
using WorkPulse.Services.Authentications.Contracts.DTOs.Responses;
using WorkPulse.Services.Authentications.Exceptions;
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
    IUserQuery userQuery,
    IIdentityService passwordHasher,
    ITokenService tokenService,
    IRefreshTokenService refreshTokenService): ControllerBase
{

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var user = await service.FindByUsername(dto.Username);
        if (user.IsExist && passwordHasher.VerifyPassword(user.Password!,
                dto.Password))
        {
            var accessToken = tokenService.GenerateToken(new BaseUserInformationDto
            {
                Username = dto.Username,
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                Email = user.Email!,
                Role = user.Role,
                Id = user.UserId
            });
            var refreshToken = await refreshTokenService.Issue(user.UserId);

            return Ok(new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
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
            var accessToken = tokenService.GenerateToken(response);
            var refreshToken = await refreshTokenService.Issue(response.Id);

            return Ok(new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
        catch (DuplicateUserException)
        {
            return Conflict($"Username '{dto.Username}' or email '{dto.Email}' already exists.");
        }
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto dto)
    {
        try
        {
            var result = await refreshTokenService.Rotate(dto.RefreshToken);
            var user = await userQuery.GetById(result.UserId);
            if (user is null)
            {
                return Unauthorized();
            }

            var accessToken = tokenService.GenerateToken(new BaseUserInformationDto
            {
                Id = result.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role
            });

            return Ok(new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = result.RefreshToken
            });
        }
        catch (InvalidRefreshTokenException)
        {
            return Unauthorized();
        }
    }

}
