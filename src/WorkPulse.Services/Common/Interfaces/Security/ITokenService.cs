using WorkPulse.Services.Users.Contracts.DTOs.Response;

namespace WorkPulse.Services.Common.Interfaces.Security;

public interface ITokenService
{
    string GenerateToken(BaseUserInformationDto user);
    string GenerateRefreshToken();
}